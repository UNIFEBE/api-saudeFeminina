-- ============================================================
-- MODELO DE BANCO DE DADOS - APP DE SAÚDE FEMININA
-- PostgreSQL
-- ============================================================

-- Extensão usada para gerar IDs (UUID) automaticamente
CREATE EXTENSION IF NOT EXISTS pgcrypto;

-- ============================================================
-- 1. USUARIO
-- Conta, dados pessoais e dados biométricos/perfil de saúde
-- (unifica login + perfil em uma única tabela)
-- ============================================================
CREATE TABLE usuario (
    id                UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    nome              VARCHAR(150) NOT NULL,
    email             VARCHAR(150) UNIQUE NOT NULL,
    senha_hash        VARCHAR(255) NOT NULL,
    data_nascimento   DATE,
    telefone          VARCHAR(20),
    cidade            VARCHAR(100),
    estado            CHAR(2),

    -- Dados de saúde (antes em perfil_saude)
    peso              DECIMAL(5,2),   -- kg  -> card "Peso Atual"
    altura            DECIMAL(5,2),   -- cm
    tipo_sanguineo    VARCHAR(3),
    fase_vida         VARCHAR(50),    -- Ex: Adolescência, Fase Adulta, Tentante,
                                       -- Gestação e Pós-parto, Climatério, Senescência

    criado_em         TIMESTAMP WITH TIME ZONE DEFAULT now(),
    atualizado_em     TIMESTAMP WITH TIME ZONE DEFAULT now()
);

-- ============================================================
-- 2. CONFIGURAÇÃO DO CICLO
-- Base para o card "Status do Ciclo" no Dashboard e
-- "Ciclo Médio" na tela de Perfil
-- Obs: dia do ciclo e fase atual (Folicular/Ovulatória/Lútea)
-- são CALCULADOS pela aplicação a partir dos dados abaixo,
-- não ficam armazenados aqui.
-- ============================================================
CREATE TABLE ciclo_config (
    id                          UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    usuario_id                  UUID UNIQUE NOT NULL REFERENCES usuario(id) ON DELETE CASCADE,
    data_ultima_menstruacao     DATE NOT NULL,
    duracao_ciclo               INT NOT NULL DEFAULT 28,  -- "Ciclo Médio"
    duracao_menstruacao         INT NOT NULL DEFAULT 5,
    ciclo_regular                BOOLEAN DEFAULT TRUE,
    atualizado_em               TIMESTAMP WITH TIME ZONE DEFAULT now()
);

-- ============================================================
-- 3. REGISTRO DIÁRIO
-- Diário de humor, sintomas e fluxo (não aparece pronto nas
-- telas enviadas, mas sustenta o cálculo de ciclo/fase)
-- ============================================================
CREATE TABLE registro_diario (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    usuario_id      UUID NOT NULL REFERENCES usuario(id) ON DELETE CASCADE,
    data_registro   DATE NOT NULL,
    humor           VARCHAR(50),
    fluxo           VARCHAR(20) DEFAULT 'Nenhum'
                    CHECK (fluxo IN ('Nenhum','Leve','Moderado','Intenso')),
    temperatura     DECIMAL(4,2),
    sintomas        TEXT[],   -- lista simples de sintomas do dia
    observacoes     TEXT,
    criado_em       TIMESTAMP WITH TIME ZONE DEFAULT now(),
    UNIQUE (usuario_id, data_registro)  -- 1 registro por dia por usuário
);

-- ============================================================
-- 4. CONSULTA
-- Botões "Agendar Consulta" / "Agendar Retorno" no Dashboard
-- ============================================================
CREATE TABLE consulta (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    usuario_id      UUID NOT NULL REFERENCES usuario(id) ON DELETE CASCADE,
    especialidade   VARCHAR(20) NOT NULL
                    CHECK (especialidade IN ('Ginecologia','Mastologia','Clínico','Obstetrícia')),
    data_consulta   DATE NOT NULL,
    status          VARCHAR(20) DEFAULT 'Confirmada'
                    CHECK (status IN ('Confirmada','Cancelada','Concluída')),
    criado_em       TIMESTAMP WITH TIME ZONE DEFAULT now()
);

-- ============================================================
-- 5. CATEGORIA DE CONTEÚDO
-- Os 4 tópicos da tela "Espaço Saber"
-- ============================================================
CREATE TABLE categoria_conteudo (
    id      UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    nome    VARCHAR(50) UNIQUE NOT NULL
            CHECK (nome IN ('Menstruação','Contracepção','Saúde Íntima','Gravidez')),
    icone   VARCHAR(50)   -- referência ao ícone exibido na tela
);

-- ============================================================
-- 6. ARTIGO
-- Conteúdos educativos listados em cada categoria
-- (Ex: "Ciclo Menstrual: Entenda as Fases")
-- ============================================================
CREATE TABLE artigo (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    categoria_id    UUID NOT NULL REFERENCES categoria_conteudo(id) ON DELETE CASCADE,
    titulo          VARCHAR(255) NOT NULL,
    resumo          TEXT NOT NULL,   -- texto curto exibido no card
    conteudo        TEXT,            -- texto completo (Markdown ou HTML)
    publicado       BOOLEAN DEFAULT TRUE,
    criado_em       TIMESTAMP WITH TIME ZONE DEFAULT now()
);

-- ============================================================
-- 7. DICA DO DIA
-- Bloco "💡 Dica do Dia" no Dashboard
-- ============================================================
CREATE TABLE dica_dia (
    id          UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    texto       TEXT NOT NULL,
    fase_ciclo  VARCHAR(20),   -- opcional: exibir dica de acordo com a fase
                                -- atual do ciclo (Folicular, Ovulatória, Lútea, Menstrual)
    ativo       BOOLEAN DEFAULT TRUE
);

-- ============================================================
-- 8. REGISTRO DE CICLO (HISTÓRICO DE PERÍODOS)
-- Cada registro representa um período menstrual completo
-- (início e término), permitindo múltiplos por usuário.
-- Usado para: visualização no calendário, cálculo de previsão
-- do próximo ciclo baseado na média dos ciclos anteriores.
-- ============================================================
CREATE TABLE ciclo_registro (
    id              UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    usuario_id      UUID NOT NULL REFERENCES usuario(id) ON DELETE CASCADE,
    data_inicio     DATE NOT NULL,
    data_fim        DATE,                -- NULL se o ciclo ainda está em andamento
    duracao_ciclo   INT,                 -- dias entre este início e o próximo (calculado ao fechar)
    observacoes     TEXT,
    criado_em       TIMESTAMP WITH TIME ZONE DEFAULT now(),
    atualizado_em   TIMESTAMP WITH TIME ZONE DEFAULT now()
);

CREATE INDEX idx_ciclo_registro_usuario ON ciclo_registro(usuario_id, data_inicio DESC);

-- ============================================================
-- ÍNDICES (consultas mais comuns do app)
-- ============================================================
CREATE INDEX idx_registro_diario_usuario_data ON registro_diario(usuario_id, data_registro);
CREATE INDEX idx_consulta_usuario             ON consulta(usuario_id);
CREATE INDEX idx_artigo_categoria             ON artigo(categoria_id);
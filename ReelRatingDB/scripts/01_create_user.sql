-- ============================================================
-- CRIAÇÃO DE USUÁRIO PARA O PROJETO ReelRatingDB
-- ============================================================

CREATE USER moviedb IDENTIFIED BY ReelRatingDB2025
    DEFAULT TABLESPACE USERS
    TEMPORARY TABLESPACE TEMP
    QUOTA UNLIMITED ON USERS;

GRANT CONNECT, RESOURCE TO moviedb;
GRANT CREATE SESSION, CREATE TABLE, CREATE VIEW, CREATE SEQUENCE TO moviedb;
GRANT UNLIMITED TABLESPACE TO moviedb;

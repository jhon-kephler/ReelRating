-- ============================================================
-- DEFINE O SCHEMA ATUAL
-- ============================================================

ALTER SESSION SET CURRENT_SCHEMA = moviedb;

-- ============================================================
-- TABELAS DO SISTEMA SelectedMovie
-- ============================================================

CREATE TABLE Customer (
    Id            NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    Nickname      VARCHAR2(100) NOT NULL,
    Name          VARCHAR2(200) NOT NULL,
    Email         VARCHAR2(200) UNIQUE NOT NULL,
    Password      VARCHAR2(200) NOT NULL,
    CreatedAt     DATE DEFAULT SYSDATE NOT NULL,
    Status        NUMBER(1) DEFAULT 1 NOT NULL
);

CREATE TABLE Type_Cine (
    Id            NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    Name          VARCHAR2(100) NOT NULL
);

CREATE TABLE Categories (
    Id            NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    Name          VARCHAR2(100) NOT NULL,
    Type_Id       NUMBER,
    CONSTRAINT FK_CATEGORY_TYPE FOREIGN KEY (Type_Id) REFERENCES Type_Cine(Id)
);

CREATE TABLE WhatchIn (
    Id            NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    Name          VARCHAR2(100) NOT NULL,
    Region        VARCHAR2(100),
    Available     NUMBER(1) DEFAULT 1 NOT NULL
);

CREATE TABLE Status (
    Id            NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    Name          VARCHAR2(100) NOT NULL
);

CREATE TABLE Cine (
    Id            NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    Name          VARCHAR2(200) NOT NULL,
    Year          NUMBER(4),
    Month         NUMBER(2),
    Whatch_Id     NUMBER,
    Type_Id       NUMBER,
    URL_Poster    VARCHAR2(500),
    TmdbId        NUMBER UNIQUE,
    CONSTRAINT FK_CINE_WHATCH FOREIGN KEY (Whatch_Id) REFERENCES WhatchIn(Id),
    CONSTRAINT FK_CINE_TYPE FOREIGN KEY (Type_Id) REFERENCES Type_Cine(Id)
);

CREATE TABLE Cine_Categories (
    Cine_Id        NUMBER NOT NULL,
    Categories_Id  NUMBER NOT NULL,
    CONSTRAINT PK_CINE_CATEGORIES PRIMARY KEY (Cine_Id, Categories_Id),
    CONSTRAINT FK_CC_CINE FOREIGN KEY (Cine_Id) REFERENCES Cine(Id),
    CONSTRAINT FK_CC_CATEGORY FOREIGN KEY (Categories_Id) REFERENCES Categories(Id)
);

CREATE TABLE Notes (
    Id              NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    Cine_Id         NUMBER NOT NULL,
    TMDB_Note       VARCHAR2(10),
    Customer_Notes  VARCHAR2(4000),
    CONSTRAINT FK_NOTES_CINE FOREIGN KEY (Cine_Id) REFERENCES Cine(Id)
);

CREATE TABLE Review (
    Id              NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    Customer_Id     NUMBER NOT NULL,
    Cine_Id         NUMBER NOT NULL,
    Categories_Id   NUMBER,
    Type_Id         NUMBER,
    Status_Id       NUMBER,
    Review          VARCHAR2(2000),
    Note            NUMBER,
    Deleted         NUMBER(1) DEFAULT 0,
    CONSTRAINT FK_REVIEW_CUSTOMER FOREIGN KEY (Customer_Id) REFERENCES Customer(Id),
    CONSTRAINT FK_REVIEW_CINE FOREIGN KEY (Cine_Id) REFERENCES Cine(Id),
    CONSTRAINT FK_REVIEW_CATEGORY FOREIGN KEY (Categories_Id) REFERENCES Categories(Id),
    CONSTRAINT FK_REVIEW_TYPE FOREIGN KEY (Type_Id) REFERENCES Type_Cine(Id),
    CONSTRAINT FK_REVIEW_STATUS FOREIGN KEY (Status_Id) REFERENCES Status(Id)
);

CREATE TABLE Favorites (
    Id              NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    Customer_Id     NUMBER NOT NULL,
    Cine_Id         NUMBER NOT NULL,
    Deleted         NUMBER(1) DEFAULT 0,
    CONSTRAINT FK_FAVORITES_CUSTOMER FOREIGN KEY (Customer_Id) REFERENCES Customer(Id),
    CONSTRAINT FK_FAVORITES_CINE FOREIGN KEY (Cine_Id) REFERENCES Cine(Id)
);

CREATE TABLE Comments (
    Id              NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    Customer_Id     NUMBER NOT NULL,
    Cine_Id         NUMBER NOT NULL,
    Comment_Text    VARCHAR2(2000),
    Deleted         NUMBER(1) DEFAULT 0,
    CONSTRAINT FK_COMMENTS_CUSTOMER FOREIGN KEY (Customer_Id) REFERENCES Customer(Id),
    CONSTRAINT FK_COMMENTS_CINE FOREIGN KEY (Cine_Id) REFERENCES Cine(Id)
);

CREATE TABLE Preferences (
    Id              NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    Customer_Id     NUMBER NOT NULL,
    Categories_Id   NUMBER NOT NULL,
    Note_Origin     VARCHAR2(200),
    CONSTRAINT FK_PREF_CUSTOMER FOREIGN KEY (Customer_Id) REFERENCES Customer(Id),
    CONSTRAINT FK_PREF_CATEGORY FOREIGN KEY (Categories_Id) REFERENCES Categories(Id)
);

CREATE TABLE Customer_Whatch (
    Id              NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    Customer_Id     NUMBER NOT NULL,
    QTT_Access      NUMBER DEFAULT 0,
    Whatch_Id       NUMBER NOT NULL,
    CONSTRAINT FK_CW_CUSTOMER FOREIGN KEY (Customer_Id) REFERENCES Customer(Id),
    CONSTRAINT FK_CW_WHATCH FOREIGN KEY (Whatch_Id) REFERENCES WhatchIn(Id)
);

CREATE TABLE Customer_Cine (
    Id              NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    Customer_Id     NUMBER NOT NULL,
    Cine_Id         NUMBER NOT NULL,
    Status_Id       NUMBER,
    CreatedAt       DATE DEFAULT SYSDATE,
    UpdatedAt       DATE DEFAULT SYSDATE,
    CONSTRAINT FK_CC_CUSTOMER FOREIGN KEY (Customer_Id) REFERENCES Customer(Id),
    CONSTRAINT FK_CC_CINE FOREIGN KEY (Cine_Id) REFERENCES Cine(Id),
    CONSTRAINT FK_CC_STATUS FOREIGN KEY (Status_Id) REFERENCES Status(Id)
);

CREATE TABLE Average_Hours (
    Id              NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    Hours_Id        NUMBER,
    Hours           NUMBER,
    Mount           NUMBER
);

CREATE TABLE Customer_Average_Hours (
    Id              NUMBER GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    Customer_Id     NUMBER NOT NULL,
    Hours           NUMBER,
    Field           VARCHAR2(100),
    CONSTRAINT FK_CAH_CUSTOMER FOREIGN KEY (Customer_Id) REFERENCES Customer(Id)
);

CREATE INDEX IDX_CINE_NAME ON Cine (Name);
CREATE INDEX IDX_REVIEW_STATUS ON Review (Status_Id);
CREATE INDEX IDX_COMMENT_CINE ON Comments (Cine_Id);
CREATE INDEX IDX_CUSTOMER_CINE ON Customer_Cine (Customer_Id, Cine_Id);

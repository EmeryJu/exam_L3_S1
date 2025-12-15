-- Création des types énumérés
CREATE TYPE etatcommande AS ENUM ('EN_COURS', 'VALIDE', 'TERMINE', 'ANNULE');
CREATE TYPE modepaiement AS ENUM ('WAVE', 'ORANGE_MONEY');
CREATE TYPE typecommande AS ENUM ('LIVRAISON', 'A_EMPORTER', 'SUR_PLACE');

-- Table Zone (ajoutée car vide dans le modèle, assumant id et nom)
CREATE TABLE zone (
    id SERIAL PRIMARY KEY,
    nom VARCHAR(255) NOT NULL
);

-- Table Client
CREATE TABLE client (
    id SERIAL PRIMARY KEY,
    nom VARCHAR(255) NOT NULL,
    prenom VARCHAR(255) NOT NULL,
    telephone VARCHAR(20),
    login VARCHAR(255) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL
);

-- Table Burger
CREATE TABLE burger (
    id SERIAL PRIMARY KEY,
    nom VARCHAR(255) NOT NULL,
    prix NUMERIC(10,2) NOT NULL,
    image VARCHAR(500),
    archive BOOLEAN DEFAULT FALSE
);

-- Table Complement
CREATE TABLE complement (
    id SERIAL PRIMARY KEY,
    nom VARCHAR(255) NOT NULL,
    prix NUMERIC(10,2) NOT NULL,
    image VARCHAR(500),
    archive BOOLEAN DEFAULT FALSE
);

-- Table Livreur
CREATE TABLE livreur (
    id SERIAL PRIMARY KEY,
    nom VARCHAR(255) NOT NULL,
    zone_id INT REFERENCES zone(id)
);

-- Table Menu
CREATE TABLE menu (
    id SERIAL PRIMARY KEY,
    nom VARCHAR(255) NOT NULL,
    image VARCHAR(500),
    burger_id INT REFERENCES burger(id),
    boisson_id INT REFERENCES complement(id),
    frites_id INT REFERENCES complement(id)
);

-- Table Commande
CREATE TABLE commande (
    id SERIAL PRIMARY KEY,
    client_id INT REFERENCES client(id),
    etat etatcommande DEFAULT 'EN_COURS',
    type typecommande NOT NULL,
    date DATE DEFAULT CURRENT_DATE,
    payee BOOLEAN DEFAULT FALSE
);

-- Table Paiement (liée à Commande)
CREATE TABLE paiement (
    id SERIAL PRIMARY KEY,
    commande_id INT REFERENCES commande(id),
    date DATE DEFAULT CURRENT_DATE,
    montant NUMERIC(10,2) NOT NULL,
    mode modepaiement NOT NULL
);

-- Tables de jonction pour les relations many-to-many
CREATE TABLE commande_burger (
    commande_id INT REFERENCES commande(id),
    burger_id INT REFERENCES burger(id),
    PRIMARY KEY (commande_id, burger_id)
);

CREATE TABLE commande_menu (
    commande_id INT REFERENCES commande(id),
    menu_id INT REFERENCES menu(id),
    PRIMARY KEY (commande_id, menu_id)
);

CREATE TABLE commande_complement (
    commande_id INT REFERENCES commande(id),
    complement_id INT REFERENCES complement(id),
    PRIMARY KEY (commande_id, complement_id)
);
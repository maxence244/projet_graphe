DROP DATABASE IF EXISTS projetSQL;
CREATE DATABASE IF NOT EXISTS projetSQL;
USE projetSQL;

CREATE TABLE Client_Individuel(
   id_client_ VARCHAR(50),
   pref_alimentaire VARCHAR(50),
   PRIMARY KEY(id_client_)
);

CREATE TABLE Livraison_(
   id_livraison VARCHAR(50),
   distance_km DECIMAL(15,2),
   temps_estime INT,
   chemin_suivi VARCHAR(50),
   statut VARCHAR(50),
   PRIMARY KEY(id_livraison)
);

CREATE TABLE Entreprise(
   id_entreprise VARCHAR(50),
   nom_entreprise VARCHAR(50),
   SIRET INT,
   mail_entreprise VARCHAR(50),
   tel_entreprise INT,
   PRIMARY KEY(id_entreprise)
);

CREATE TABLE Ingrédient(
   id_ingrédients VARCHAR(50),
   nom_ingrédient VARCHAR(50),
   allergène VARCHAR(50),
   type VARCHAR(50),
   quantité_alim INT,
   PRIMARY KEY(id_ingrédients)
);

CREATE TABLE métro(
   id_station VARCHAR(50),
   nom_station VARCHAR(50),
   ligne VARCHAR(50),
   id_livraison VARCHAR(50) NOT NULL,
   PRIMARY KEY(id_station),
   FOREIGN KEY(id_livraison) REFERENCES Livraison_(id_livraison)
);

CREATE TABLE recette(
   id_recette VARCHAR(50),
   régime_alimentaire VARCHAR(50),
   nationalité VARCHAR(50),
   nom_recette VARCHAR(50),
   type VARCHAR(50),
   PRIMARY KEY(id_recette)
);

CREATE TABLE Commande(
   id_commande_ VARCHAR(50),
   date_commande_ VARCHAR(50),
   prix_total DECIMAL(15,2),
   statut VARCHAR(50),
   paiment VARCHAR(50),
   historique VARCHAR(50),
   id_livraison VARCHAR(50) NOT NULL,
   id_client_ VARCHAR(50),
   id_entreprise VARCHAR(50),
   PRIMARY KEY(id_commande_),
   FOREIGN KEY(id_livraison) REFERENCES Livraison_(id_livraison),
   FOREIGN KEY(id_client_) REFERENCES Client_Individuel(id_client_),
   FOREIGN KEY(id_entreprise) REFERENCES Entreprise(id_entreprise)
);

CREATE TABLE Plat(
   id_plat VARCHAR(50),
   prix DECIMAL(15,2),
   date_fabrication VARCHAR(50),
   date_péremption VARCHAR(50),
   quantité INT,
   id_recette VARCHAR(50) NOT NULL,
   PRIMARY KEY(id_plat),
   FOREIGN KEY(id_recette) REFERENCES recette(id_recette)
);

CREATE TABLE paiement(
   id_paiement VARCHAR(50),
   montant DECIMAL(15,2),
   moyent_paiement VARCHAR(50),
   date_paiement VARCHAR(50),
   id_commande_ VARCHAR(50) NOT NULL,
   PRIMARY KEY(id_paiement),
   FOREIGN KEY(id_commande_) REFERENCES Commande(id_commande_)
);

CREATE TABLE Cuisinier(
   id_cuisinier_ VARCHAR(50),
   spe_cuisine VARCHAR(50),
   année_exp INT,
   certif VARCHAR(50),
   note_moyenne DECIMAL(15,2),
   nb_livraison INT,
   id_commande_ VARCHAR(50),
   PRIMARY KEY(id_cuisinier_),
   FOREIGN KEY(id_commande_) REFERENCES Commande(id_commande_)
);

CREATE TABLE Individu(
   id_1 VARCHAR(50),
   prenom VARCHAR(50),
   nom VARCHAR(50),
   email VARCHAR(50),
   telephone INT,
   id_cuisinier_ VARCHAR(50) NOT NULL,
   id_client_ VARCHAR(50) NOT NULL,
   id_entreprise VARCHAR(50) NOT NULL,
   PRIMARY KEY(id_1),
   UNIQUE(id_cuisinier_),
   UNIQUE(id_client_),
   UNIQUE(id_entreprise),
   FOREIGN KEY(id_cuisinier_) REFERENCES Cuisinier(id_cuisinier_),
   FOREIGN KEY(id_client_) REFERENCES Client_Individuel(id_client_),
   FOREIGN KEY(id_entreprise) REFERENCES Entreprise(id_entreprise)
);

CREATE TABLE Adresse(
   id_adresse VARCHAR(50),
   rue VARCHAR(50),
   ville VARCHAR(50),
   code_postal INT,
   pays VARCHAR(50),
   type_adresse VARCHAR(50),
   numéro INT,
   id_1 VARCHAR(50) NOT NULL,
   PRIMARY KEY(id_adresse),
   FOREIGN KEY(id_1) REFERENCES Individu(id_1)
);

CREATE TABLE contient(
   id_plat VARCHAR(50),
   adresse VARCHAR(50),
   id_commande_ VARCHAR(50) NOT NULL,
   PRIMARY KEY(id_plat),
   FOREIGN KEY(id_plat) REFERENCES Plat(id_plat),
   FOREIGN KEY(id_commande_) REFERENCES Commande(id_commande_)
);

CREATE TABLE composée(
   id_ingrédients VARCHAR(50),
   id_recette VARCHAR(50),
   PRIMARY KEY(id_ingrédients, id_recette),
   FOREIGN KEY(id_ingrédients) REFERENCES Ingrédient(id_ingrédients),
   FOREIGN KEY(id_recette) REFERENCES recette(id_recette)
);

CREATE TABLE Fait(
   id_cuisinier_ VARCHAR(50),
   id_livraison VARCHAR(50),
   PRIMARY KEY(id_cuisinier_, id_livraison),
   FOREIGN KEY(id_cuisinier_) REFERENCES Cuisinier(id_cuisinier_),
   FOREIGN KEY(id_livraison) REFERENCES Livraison_(id_livraison)
);

CREATE TABLE est_dans_(
   id_livraison VARCHAR(50),
   id_plat VARCHAR(50),
   PRIMARY KEY(id_livraison, id_plat),
   FOREIGN KEY(id_livraison) REFERENCES Livraison_(id_livraison),
   FOREIGN KEY(id_plat) REFERENCES Plat(id_plat)
);

CREATE TABLE avis_post_commande(
   id_client_ VARCHAR(50),
   id_cuisinier_ VARCHAR(50),
   nb_étoile INT,
   commentaire VARCHAR(50),
   date_avis VARCHAR(50),
   PRIMARY KEY(id_client_, id_cuisinier_),
   FOREIGN KEY(id_client_) REFERENCES Client_Individuel(id_client_),
   FOREIGN KEY(id_cuisinier_) REFERENCES Cuisinier(id_cuisinier_)
);


-- Ajout de Clients Individuels
INSERT INTO Client_Individuel (id_client_, pref_alimentaire) VALUES
('C1', 'Végétarien'),
('C2', 'Sans gluten'),
('C3', 'Halal');

-- Ajout de Cuisiniers
INSERT INTO Cuisinier (id_cuisinier_, spe_cuisine, année_exp, certif, note_moyenne, nb_livraison, id_commande_) VALUES
('CU1', 'Italienne', 5, 'Certificat A', 4.5, 30, NULL),
('CU2', 'Japonaise', 10, 'Certificat B', 4.8, 50, NULL),
('CU3', 'Française', 7, 'Certificat C', 4.2, 40, NULL);

-- Ajout d'Entreprises
INSERT INTO Entreprise (id_entreprise, nom_entreprise, SIRET, mail_entreprise, tel_entreprise) VALUES
('E1', 'RestoRapide', 123456789, 'contact@restorapide.com', 987654321),
('E2', 'SaveursDuMonde', 987654321, 'info@saveursdumonde.com', 123456789);

-- Ajout de Livraisons
INSERT INTO Livraison_ (id_livraison, distance_km, temps_estime, chemin_suivi, statut) VALUES
('L1', 5.2, 15, 'Rue des Oliviers', 'En cours'),
('L2', 12.0, 30, 'Avenue du Général', 'Livré');

-- Ajout de Commandes
INSERT INTO Commande (id_commande_, date_commande_, prix_total, statut, paiment, historique, id_livraison, id_client_, id_entreprise) VALUES
('CMD1', '2025-02-25', 35.50, 'Livré', 'Carte bancaire', 'Commande réussie', 'L1', 'C1', 'E1'),
('CMD2', '2025-02-26', 50.00, 'En cours', 'PayPal', 'En attente', 'L2', 'C2', 'E2');

-- Ajout de Recettes
INSERT INTO recette (id_recette, régime_alimentaire, nationalité, nom_recette, type) VALUES
('R1', 'Végétarien', 'Française', 'Quiche Lorraine', 'Plat Principal'),
('R2', 'Sans gluten', 'Italienne', 'Pizza Margherita', 'Plat Principal');

-- Ajout de Plats
INSERT INTO Plat (id_plat, prix, date_fabrication, date_péremption, quantité, id_recette) VALUES  
('P1', 15.00, '2025-02-24', '2025-02-28', 10, 'R1'),  
('P2', 12.50, '2025-02-23', '2025-02-27', 5, 'R2');


-- Ajout d'Ingrédients
INSERT INTO Ingrédient (id_ingrédients, nom_ingrédient, allergène, type, quantité_alim) VALUES
('I1', 'Tomate', 'Aucun', 'Légume', 100),
('I2', 'Fromage', 'Lactose', 'Produit laitier', 50),
('I3', 'Pain pita', 'Gluten', 'Céréale', 30);

-- Ajout de Stations de Métro
INSERT INTO métro (id_station, nom_station, ligne, id_livraison) VALUES
('M1', 'Opéra', 'Ligne 1', 'L1'),
('M2', 'Bastille', 'Ligne 5', 'L2');

-- Ajout de Paiements
INSERT INTO paiement (id_paiement, montant, moyent_paiement, date_paiement, id_commande_) VALUES
('PAY1', 35.50, 'Carte bancaire', '2025-02-25', 'CMD1'),
('PAY2', 50.00, 'PayPal', '2025-02-26', 'CMD2');

-- Ajout d'Avis post-commande (Client → Cuisinier)
INSERT INTO avis_post_commande (id_client_, id_cuisinier_, nb_étoile, commentaire, date_avis) VALUES
('C1', 'CU1', 5, 'Excellent plat !', '2025-02-26'),
('C2', 'CU2', 4, 'Très bon mais un peu épicé', '2025-02-27');

-- Ajout d'Individus
INSERT INTO Individu (id_1, prenom, nom, email, telephone, id_cuisinier_, id_client_, id_entreprise) VALUES
('I1', 'Alice', 'Durand', 'alice.durand@mail.com', 123456789, 'CU1', 'C1', 'E1'),
('I2', 'Marc', 'Leroy', 'marc.leroy@mail.com', 987654321, 'CU2', 'C2', 'E2');

-- Ajout d'Adresses
INSERT INTO Adresse (id_adresse, rue, ville, code_postal, pays, type_adresse, numéro, id_1) VALUES
('A1', 'Rue de Rivoli', 'Paris', 75001, 'France', 'Domicile', 12, 'I1'),
('A2', 'Avenue des Champs', 'Paris', 75008, 'France', 'Travail', 45, 'I2');

-- Ajout des relations entre les tables associatives
INSERT INTO composée (id_ingrédients, id_recette) VALUES
('I1', 'R1'),
('I2', 'R1'),
('I3', 'R2');

INSERT INTO contient (id_plat, adresse, id_commande_) VALUES
('P1', 'A1', 'CMD1'),
('P2', 'A2', 'CMD2');

INSERT INTO Fait (id_cuisinier_, id_livraison) VALUES
('CU1', 'L1'),
('CU2', 'L2');

INSERT INTO est_dans_ (id_livraison, id_plat) VALUES
('L1', 'P1'),
('L2', 'P2');


-- Affichage de toutes les tables
SELECT * FROM Client_Individuel;
SELECT * FROM Cuisinier;
SELECT * FROM Entreprise;
SELECT * FROM Livraison_;
SELECT * FROM Commande;
SELECT * FROM Plat;
SELECT * FROM recette;
SELECT * FROM Ingrédient;
SELECT * FROM métro;
SELECT * FROM paiement;
SELECT * FROM avis_post_commande;
SELECT * FROM Individu;
SELECT * FROM Adresse;
SELECT * FROM composée;
SELECT * FROM contient;
SELECT * FROM Fait;
SELECT * FROM est_dans_;

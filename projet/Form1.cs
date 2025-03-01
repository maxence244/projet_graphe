using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace projet
{
    public partial class Form1 : Form
    {
        private Graphe _graphe;
        private VisualiseurGraphe _visualiseur;
        private Label _etiquetteResultat;
        private TextBox _noeudDepartParcoursLargeur;
        private TextBox _noeudDepartParcoursProfondeur;
        private TextBox _noeudDepartCheminCourt;
        private TextBox _noeudArriveeCheminCourt;
        private Button _boutonVerifierConnexite;
        private Button _boutonParcoursLargeur;
        private Button _boutonParcoursProfondeur;
        private Button _boutonCheminCourt;
        private Button _boutonReinitialiser;
        private Button _boutonAfficherListeAdjacence;
        private Button _boutonAfficherMatriceAdjacence;
        private TextBox _zoneTexteResultat;
        private Label _etiquetteOrdre;
        private Label _etiquetteTaille;

        /// <summary>
        /// Constructeur de la classe Form1.
        /// Initialise les composants et le graphe.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            InitialiserInterfaceUtilisateur();
            InitialiserGraphe();
        }


        /// <summary>
        /// Initialise l'interface utilisateur avec les composants nécessaires.
        /// </summary>
        private void InitialiserInterfaceUtilisateur()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Text = "Visualisation du Graphe";
            this.Size = new Size(1200, 800);
            this.Paint += new PaintEventHandler(Form1_Paint);

            // Zone pour afficher les résultats des parcours
            _etiquetteResultat = CreerEtiquette("Résultats des Parcours :", new Point(850, 10), new Font("Arial", 12, FontStyle.Bold));

            // Champs de texte pour les parcours et le chemin le plus court
            _noeudDepartParcoursLargeur = CreerTextBox(new Point(850, 60));
            _noeudDepartParcoursProfondeur = CreerTextBox(new Point(850, 110));
            _noeudDepartCheminCourt = CreerTextBox(new Point(850, 160));
            _noeudArriveeCheminCourt = CreerTextBox(new Point(970, 160));

            // Étiquettes pour les champs de texte
            CreerEtiquette("Départ Parcours Largeur:", new Point(850, 40));
            CreerEtiquette("Départ Parcours Profondeur:", new Point(850, 90));
            CreerEtiquette("Départ Chemin Court:", new Point(850, 140));
            CreerEtiquette("Arrivée Chemin Court:", new Point(970, 140));

            // Boutons pour les différentes fonctionnalités
            _boutonVerifierConnexite = CreerBouton("Vérifier Connexité", new Point(850, 200), BoutonVerifierConnexite_Click);
            _boutonParcoursLargeur = CreerBouton("Parcours Largeur", new Point(850, 250), BoutonParcoursLargeur_Click);
            _boutonParcoursProfondeur = CreerBouton("Parcours Profondeur", new Point(850, 300), BoutonParcoursProfondeur_Click);
            _boutonCheminCourt = CreerBouton("Chemin Court", new Point(850, 350), BoutonCheminCourt_Click);
            _boutonReinitialiser = CreerBouton("Réinitialiser", new Point(850, 400), BoutonReinitialiser_Click);
            _boutonAfficherListeAdjacence = CreerBouton("Afficher Liste d'Adjacence", new Point(850, 450), BoutonAfficherListeAdjacence_Click);
            _boutonAfficherMatriceAdjacence = CreerBouton("Afficher Matrice d'Adjacence", new Point(850, 500), BoutonAfficherMatriceAdjacence_Click);

            // Zone pour afficher les résultats des parcours
            _zoneTexteResultat = CreerZoneTexte(new Point(1280, 20), new Size(600, 700));

            // Étiquettes pour afficher les informations du graphe
            _etiquetteOrdre = CreerEtiquette("Ordre du graphe : ", new Point(850, 550));
            _etiquetteTaille = CreerEtiquette("Taille du graphe : ", new Point(850, 570));

            // Mettre à jour les étiquettes avec les informations du graphe
            MettreAJourEtiquettesGraphe();
        }


        /// <summary>
        /// Crée une étiquette avec les propriétés spécifiées.
        /// </summary>
        /// <param name="texte">Le texte de l'étiquette.</param>
        /// <param name="position">La position de l'étiquette.</param>
        /// <param name="font">La police de l'étiquette.</param>
        /// <returns>L'étiquette créée.</returns>
        private Label CreerEtiquette(string texte, Point position, Font font = null)
        {
            Label etiquette = new Label
            {
                Location = position,
                Size = new Size(300, 20),
                Font = font ?? new Font("Arial", 10),
                Text = texte
            };
            this.Controls.Add(etiquette);
            return etiquette;
        }


        /// <summary>
        /// Crée un bouton avec les propriétés spécifiées.
        /// </summary>
        /// <param name="texte">Le texte du bouton.</param>
        /// <param name="position">La position du bouton.</param>
        /// <param name="gestionnaireClic">Le gestionnaire d'événements pour le clic du bouton.</param>
        /// <returns>Le bouton créé.</returns>
        private Button CreerBouton(string texte, Point position, EventHandler gestionnaireClic)
        {
            Button bouton = new Button
            {
                Text = texte,
                Location = position,
                Size = new Size(150, 30),
                FlatStyle = FlatStyle.Flat
            };
            bouton.Click += gestionnaireClic;
            this.Controls.Add(bouton);
            return bouton;
        }

        /// <summary>
        /// Crée une zone de texte avec les propriétés spécifiées.
        /// </summary>
        /// <param name="position">La position de la zone de texte.</param>
        /// <param name="taille">La taille de la zone de texte.</param>
        /// <returns>La zone de texte créée.</returns>
        private TextBox CreerTextBox(Point position, Size taille = default)
        {
            TextBox textBox = new TextBox
            {
                Location = position,
                Size = taille == default ? new Size(100, 20) : taille
            };
            this.Controls.Add(textBox);
            return textBox;
        }


        /// <summary>
        /// Crée une zone de texte multiligne pour afficher les résultats.
        /// </summary>
        /// <param name="position">La position de la zone de texte.</param>
        /// <param name="taille">La taille de la zone de texte.</param>
        /// <returns>La zone de texte créée.</returns>
        private TextBox CreerZoneTexte(Point position, Size taille)
        {
            TextBox textBox = new TextBox
            {
                Location = position,
                Size = taille,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Arial", 10),
                ReadOnly = true
            };
            this.Controls.Add(textBox);
            return textBox;
        }


        /// <summary>
        /// Gestionnaire d'événements pour le bouton "Vérifier Connexité".
        /// </summary>
        private void BoutonVerifierConnexite_Click(object sender, EventArgs e)
        {
            if (_graphe != null)
            {
                bool estConnexe = _graphe.EstConnexe();
                _zoneTexteResultat.Text += $"Le graphe est {(estConnexe ? "" : "non ")}connexe.\n";
            }
            else
            {
                MessageBox.Show("Le graphe n'est pas initialisé.");
            }
        }


        /// <summary>
        /// Gestionnaire d'événements pour le bouton "Parcours Largeur".
        /// </summary>
        private void BoutonParcoursLargeur_Click(object sender, EventArgs e)
        {
            if (_graphe != null && int.TryParse(_noeudDepartParcoursLargeur.Text, out int noeudDepartLargeur))
            {
                var cheminLargeur = _graphe.ParcoursLargeur(noeudDepartLargeur);
                _zoneTexteResultat.Text += $"Chemin Parcours Largeur (départ {noeudDepartLargeur}) : {string.Join(" -> ", cheminLargeur)}\n";
                _visualiseur.Dessiner(CreateGraphics());
            }
            else
            {
                MessageBox.Show("Veuillez entrer une valeur valide pour le nud de départ Parcours Largeur.");
            }
        }


        /// <summary>
        /// Gestionnaire d'événements pour le bouton "Parcours Profondeur".
        /// </summary>
        private void BoutonParcoursProfondeur_Click(object sender, EventArgs e)
        {
            if (_graphe != null && int.TryParse(_noeudDepartParcoursProfondeur.Text, out int noeudDepartProfondeur))
            {
                var cheminProfondeur = _graphe.ParcoursProfondeur(noeudDepartProfondeur);
                _zoneTexteResultat.Text += $"Chemin Parcours Profondeur (départ {noeudDepartProfondeur}) : {string.Join(" -> ", cheminProfondeur)}\n";
                _visualiseur.Dessiner(CreateGraphics());
            }
            else
            {
                MessageBox.Show("Veuillez entrer une valeur valide pour le nud de départ Parcours Profondeur.");
            }
        }


        /// <summary>
        /// Gestionnaire d'événements pour le bouton "Chemin Court".
        /// </summary>
        private void BoutonCheminCourt_Click(object sender, EventArgs e)
        {
            if (_graphe != null && int.TryParse(_noeudDepartCheminCourt.Text, out int noeudDepartCourt) && int.TryParse(_noeudArriveeCheminCourt.Text, out int noeudArriveeCourt))
            {
                var cheminCourt = _graphe.TrouverCheminCourt(noeudDepartCourt, noeudArriveeCourt);
                if (cheminCourt != null)
                {
                    foreach (var lien in _graphe.Liens)
                    {
                        lien.Couleur = Color.Black; // Réinitialiser la couleur
                        lien.Largeur = 1; // Réinitialiser la largeur
                    }

                    for (int i = 0; i < cheminCourt.Count - 1; i++)
                    {
                        var lien = _graphe.Liens.FirstOrDefault(l =>
                            (l.Noeud1.Id == cheminCourt[i] && l.Noeud2.Id == cheminCourt[i + 1]) ||
                            (l.Noeud2.Id == cheminCourt[i] && l.Noeud1.Id == cheminCourt[i + 1]));

                        if (lien != null)
                        {
                            lien.Couleur = Color.Green;
                            lien.Largeur = 3;
                        }
                    }

                    _zoneTexteResultat.Text += $"Chemin court de {noeudDepartCourt} à {noeudArriveeCourt} : {string.Join(" -> ", cheminCourt)}\n";
                    _visualiseur.Dessiner(CreateGraphics());
                }
                else
                {
                    _zoneTexteResultat.Text += "Aucun chemin trouvé.\n";
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer des valeurs valides pour les nuds de départ et d'arrivée.");
            }
        }


        /// <summary>
        /// Gestionnaire d'événements pour le bouton "Réinitialiser".
        /// </summary>
        private void BoutonReinitialiser_Click(object sender, EventArgs e)
        {
            if (_graphe != null)
            {
                foreach (var lien in _graphe.Liens)
                {
                    lien.Couleur = Color.Black; // Réinitialiser la couleur
                    lien.Largeur = 1; // Réinitialiser la largeur
                }
                _zoneTexteResultat.Clear();
                _visualiseur.Dessiner(CreateGraphics());
            }
            else
            {
                MessageBox.Show("Le graphe n'est pas initialisé.");
            }
        }


        /// <summary>
        /// Gestionnaire d'événements pour le bouton "Afficher Liste d'Adjacence".
        /// </summary>
        private void BoutonAfficherListeAdjacence_Click(object sender, EventArgs e)
        {
            if (_graphe != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var noeud in _graphe.ListeAdjacence)
                {
                    sb.AppendLine($"{noeud.Key}: {string.Join(", ", noeud.Value)}");
                }
                _zoneTexteResultat.Text = sb.ToString();
            }
            else
            {
                MessageBox.Show("Le graphe n'est pas initialisé.");
            }
        }


        /// <summary>
        /// Gestionnaire d'événements pour le bouton "Afficher Matrice d'Adjacence".
        /// </summary>
        private void BoutonAfficherMatriceAdjacence_Click(object sender, EventArgs e)
        {
            if (_graphe != null)
            {
                StringBuilder sb = new StringBuilder();
                int n = _graphe.Noeuds.Count;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        sb.Append(_graphe.MatriceAdjacence[i, j] ? "1 " : "0 ");
                    }
                    sb.AppendLine();
                }
                _zoneTexteResultat.Text = sb.ToString();
            }
            else
            {
                MessageBox.Show("Le graphe n'est pas initialisé.");
            }
        }


        /// <summary>
        /// Initialise le graphe avec des nuds et des liens prédéfinis.
        /// </summary>
        private void InitialiserGraphe()
        {
            _graphe = new Graphe();

            string[] lignes = {
                "2 1", "3 1", "4 1", "5 1", "6 1", "7 1", "8 1", "9 1", "11 1", "12 1", "13 1", "14 1", "18 1", "20 1", "22 1", "32 1",
                "3 2", "4 2", "8 2", "14 2", "18 2", "20 2", "22 2", "31 2",
                "4 3", "8 3", "9 3", "10 3", "14 3", "28 3", "29 3", "33 3",
                "8 4", "13 4", "14 4",
                "7 5", "11 5",
                "7 6", "11 6", "17 6",
                "17 7",
                "31 9", "33 9", "34 9",
                "34 10",
                "34 14",
                "33 15", "34 15",
                "33 16", "34 16",
                "33 19", "34 19",
                "34 20",
                "33 21", "34 21",
                "33 23", "34 23",
                "26 24", "28 24", "30 24", "33 24", "34 24",
                "26 25", "28 25", "32 25",
                "32 26",
                "30 27", "34 27",
                "34 28",
                "32 29", "34 29",
                "33 30", "34 30",
                "33 31", "34 31",
                "33 32", "34 32",
                "34 33"
            };

            foreach (var ligne in lignes)
            {
                var ids = ligne.Split(' ');
                int id1 = int.Parse(ids[0]);
                int id2 = int.Parse(ids[1]);
                _graphe.AjouterLien(id1, id2);
            }

            int rayon = 300;
            Point centre = new Point(this.ClientSize.Width / 2 - 200, this.ClientSize.Height / 2);
            _visualiseur = new VisualiseurGraphe(_graphe, rayon, centre);

            // Mettre à jour les étiquettes avec les informations du graphe
            MettreAJourEtiquettesGraphe();
        }


        /// <summary>
        /// Gestionnaire d'événements pour la peinture du formulaire.
        /// </summary>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (_visualiseur != null)
            {
                _visualiseur.Dessiner(e.Graphics);
            }
        }


        /// <summary>
        /// Gestionnaire d'événements pour le chargement du formulaire.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Load += new System.EventHandler(this.Form1_Load);
        }


        /// <summary>
        /// Met à jour les étiquettes avec les informations du graphe.
        /// </summary>
        private void MettreAJourEtiquettesGraphe()
        {
            if (_graphe != null)
            {
                _etiquetteOrdre.Text = $"Ordre du graphe : {_graphe.OrdreGraphe}";
                _etiquetteTaille.Text = $"Taille du graphe : {_graphe.TailleGraphe}";
            }
            else
            {
                _etiquetteOrdre.Text = "Ordre du graphe : Non initialisé";
                _etiquetteTaille.Text = "Taille du graphe : Non initialisé";
            }
        }
    }
}

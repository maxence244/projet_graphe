using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace projet
{
    public class Graphe
    {
        /// <summary>
        /// Dictionnaire des nœuds du graphe.
        /// </summary>
        public Dictionary<int, Noeud> Noeuds { get; } = new Dictionary<int, Noeud>();


        /// <summary>
        /// Liste des liens du graphe.
        /// </summary>
        public List<Lien> Liens { get; } = new List<Lien>();


        /// <summary>
        /// Liste d'adjacence du graphe.
        /// </summary>
        public Dictionary<int, List<int>> ListeAdjacence { get; private set; } = new Dictionary<int, List<int>>();


        /// <summary>
        /// Matrice d'adjacence du graphe.
        /// </summary>
        public bool[,] MatriceAdjacence { get; private set; }


        /// <summary>
        /// Constructeur de la classe Graphe.
        /// Initialise la matrice d'adjacence avec une taille maximale estimée.
        /// </summary>
        public Graphe()
        {
            // Initialiser la matrice d'adjacence avec une taille maximale estimée
            MatriceAdjacence = new bool[100, 100];
        }


        /// <summary>
        /// Ajoute un lien entre deux nœuds.
        /// </summary>
        /// <param name="id1">L'identifiant du premier nœud.</param>
        /// <param name="id2">L'identifiant du deuxième nœud.</param>
        public void AjouterLien(int id1, int id2)
        {
            if (!Noeuds.ContainsKey(id1))
                Noeuds[id1] = new Noeud(id1);
            if (!Noeuds.ContainsKey(id2))
                Noeuds[id2] = new Noeud(id2);

            Liens.Add(new Lien(Noeuds[id1], Noeuds[id2]));

            // Mettre à jour la liste d'adjacence
            if (!ListeAdjacence.ContainsKey(id1))
                ListeAdjacence[id1] = new List<int>();
            if (!ListeAdjacence.ContainsKey(id2))
                ListeAdjacence[id2] = new List<int>();
            ListeAdjacence[id1].Add(id2);
            ListeAdjacence[id2].Add(id1);

            // Mettre à jour la matrice d'adjacence
            MatriceAdjacence[id1, id2] = true;
            MatriceAdjacence[id2, id1] = true;
        }


        /// <summary>
        /// Effectue un parcours en profondeur (DFS) à partir d'un nœud donné.
        /// </summary>
        /// <param name="noeudDepart">L'identifiant du nœud de départ.</param>
        /// <returns>La liste des nœuds visités dans l'ordre du parcours.</returns>
        public List<int> ParcoursProfondeur(int noeudDepart)
        {
            var chemin = new List<int>();
            var visite = new HashSet<int>();
            ParcoursProfondeurRecursif(noeudDepart, visite, chemin);
            return chemin;
        }


        /// <summary>
        /// Méthode récursive pour le parcours en profondeur (DFS).
        /// </summary>
        /// <param name="noeudId">L'identifiant du nœud courant.</param>
        /// <param name="visite">L'ensemble des nœuds déjà visités.</param>
        /// <param name="chemin">La liste des nœuds visités dans l'ordre du parcours.</param>
        private void ParcoursProfondeurRecursif(int noeudId, HashSet<int> visite, List<int> chemin)
        {
            visite.Add(noeudId);
            chemin.Add(noeudId);
            foreach (var voisinId in ListeAdjacence[noeudId])
            {
                if (!visite.Contains(voisinId))
                {
                    ParcoursProfondeurRecursif(voisinId, visite, chemin);
                }
            }
        }


        /// <summary>
        /// Effectue un parcours en largeur (BFS) à partir d'un nœud donné.
        /// </summary>
        /// <param name="noeudDepart">L'identifiant du nœud de départ.</param>
        /// <returns>La liste des nœuds visités dans l'ordre du parcours.</returns>
        public List<int> ParcoursLargeur(int noeudDepart)
        {
            var visite = new HashSet<int>();
            var file = new Queue<int>();
            var chemin = new List<int>();
            file.Enqueue(noeudDepart);
            visite.Add(noeudDepart);

            while (file.Count > 0)
            {
                var noeudId = file.Dequeue();
                chemin.Add(noeudId);

                foreach (var voisinId in ListeAdjacence[noeudId])
                {
                    if (!visite.Contains(voisinId))
                    {
                        file.Enqueue(voisinId);
                        visite.Add(voisinId);
                    }
                }
            }
            return chemin;
        }


        /// <summary>
        /// Trouve le chemin le plus court entre deux nœuds en utilisant BFS.
        /// </summary>
        /// <param name="noeudDepart">L'identifiant du nœud de départ.</param>
        /// <param name="noeudArrivee">L'identifiant du nœud d'arrivée.</param>
        /// <returns>La liste des nœuds formant le chemin le plus court, ou null si aucun chemin n'existe.</returns>
        public List<int> TrouverCheminCourt(int noeudDepart, int noeudArrivee)
        {
            var parents = new Dictionary<int, int>();
            var visite = new HashSet<int>();
            var file = new Queue<int>();
            file.Enqueue(noeudDepart);
            visite.Add(noeudDepart);

            while (file.Count > 0)
            {
                var noeudId = file.Dequeue();
                foreach (var voisinId in ListeAdjacence[noeudId])
                {
                    if (!visite.Contains(voisinId))
                    {
                        parents[voisinId] = noeudId;
                        file.Enqueue(voisinId);
                        visite.Add(voisinId);

                        if (voisinId == noeudArrivee)
                        {
                            var chemin = new List<int>();
                            var current = noeudArrivee;
                            while (current != noeudDepart)
                            {
                                chemin.Insert(0, current);
                                current = parents[current];
                            }
                            chemin.Insert(0, noeudDepart);
                            return chemin;
                        }
                    }
                }
            }
            return null;
        }


        /// <summary>
        /// Vérifie si le graphe est connexe.
        /// </summary>
        /// <returns>True si le graphe est connexe, sinon False.</returns>
        public bool EstConnexe()
        {
            if (Noeuds.Count == 0)
                return true;

            var visite = new HashSet<int>();
            var pile = new Stack<int>();
            pile.Push(Noeuds.First().Key); // Commencer par n'importe quel noeud

            while (pile.Count > 0)
            {
                var noeudId = pile.Pop();
                if (!visite.Contains(noeudId))
                {
                    visite.Add(noeudId);
                    foreach (var voisinId in ListeAdjacence[noeudId])
                    {
                        if (!visite.Contains(voisinId))
                            pile.Push(voisinId);
                    }
                }
            }

            return visite.Count == Noeuds.Count;
        }


        /// <summary>
        /// Vérifie si le graphe contient des cycles.
        /// </summary>
        /// <returns>True si le graphe contient des cycles, sinon False.</returns>
        public bool ContientDesCycles()
        {
            var visite = new HashSet<int>();
            var pile = new Stack<Tuple<int, int>>(); // Pile pour DFS avec (noeud, parent)

            foreach (var noeud in Noeuds.Values)
            {
                if (!visite.Contains(noeud.Id))
                {
                    if (ParcoursProfondeur(noeud.Id, -1, visite, pile))
                        return true;
                }
            }

            return false;
        }


        /// <summary>
        /// Méthode DFS pour détecter les cycles.
        /// </summary>
        /// <param name="noeudId">L'identifiant du nœud courant.</param>
        /// <param name="parentId">L'identifiant du nœud parent.</param>
        /// <param name="visite">L'ensemble des nœuds déjà visités.</param>
        /// <param name="pile">La pile utilisée pour le parcours DFS.</param>
        /// <returns>True si un cycle est détecté, sinon False.</returns>
        private bool ParcoursProfondeur(int noeudId, int parentId, HashSet<int> visite, Stack<Tuple<int, int>> pile)
        {
            visite.Add(noeudId);
            foreach (var voisinId in ListeAdjacence[noeudId])
            {
                if (!visite.Contains(voisinId))
                {
                    pile.Push(new Tuple<int, int>(noeudId, voisinId));
                    if (ParcoursProfondeur(voisinId, noeudId, visite, pile))
                        return true;
                }
                else if (voisinId != parentId) // Cycle détecté
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// Obtient le nombre de nœuds dans le graphe.
        /// </summary>
        public int OrdreGraphe => Noeuds.Count;


        /// <summary>
        /// Obtient le nombre de liens dans le graphe.
        /// </summary>
        public int TailleGraphe => Liens.Count;
    }
}

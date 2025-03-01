using System.Drawing;

namespace projet
{
    public class Lien
    {
        /// <summary>
        /// Premier nœud du lien.
        /// </summary>
        public Noeud Noeud1 { get; }


        /// <summary>
        /// Deuxième nœud du lien.
        /// </summary>
        public Noeud Noeud2 { get; }


        /// <summary>
        /// Couleur du lien.
        /// </summary>
        public Color Couleur { get; set; }


        /// <summary>
        /// Largeur du lien.
        /// </summary>
        public int Largeur { get; set; }


        /// <summary>
        /// Constructeur de la classe Lien.
        /// Initialise les nœuds du lien et les propriétés par défaut.
        /// </summary>
        /// <param name="noeud1">Le premier nœud du lien.</param>
        /// <param name="noeud2">Le deuxième nœud du lien.</param>
        public Lien(Noeud noeud1, Noeud noeud2)
        {
            Noeud1 = noeud1;
            Noeud2 = noeud2;
            Couleur = Color.Black;
            Largeur = 1;  // Largeur initiale par défaut
        }
    }
}

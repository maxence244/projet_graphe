using System.Drawing;

namespace projet
{
    public class Noeud
    {
        /// <summary>
        /// Identifiant unique du nœud.
        /// </summary>
        public int Id { get; }


        /// <summary>
        /// Position du nœud sur le canevas.
        /// </summary>
        public Point Position { get; set; }


        /// <summary>
        /// Constructeur de la classe Noeud.
        /// Initialise l'identifiant du nœud.
        /// </summary>
        /// <param name="id">L'identifiant du nœud.</param>
        public Noeud(int id)
        {
            Id = id;
        }
    }
}

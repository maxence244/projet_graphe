using System.Drawing;

namespace projet
{
    public class VisualiseurGraphe
    {
        private readonly Graphe _graphe;
        private readonly int _rayon;
        private readonly Point _centre;


        /// <summary>
        /// Constructeur de la classe VisualiseurGraphe.
        /// Initialise le graphe et calcule les positions des nœuds.
        /// </summary>
        /// <param name="graphe">Le graphe à visualiser.</param>
        /// <param name="rayon">Le rayon du cercle pour positionner les nœuds.</param>
        /// <param name="centre">Le centre du cercle.</param>
        public VisualiseurGraphe(Graphe graphe, int rayon, Point centre)
        {
            _graphe = graphe;
            _rayon = rayon;
            _centre = centre;
            CalculerPositions();
        }


        /// <summary>
        /// Calcule les positions des nœuds en les plaçant sur un cercle.
        /// </summary>
        private void CalculerPositions()
        {
            int n = _graphe.Noeuds.Count;
            int i = 0;
            foreach (var noeud in _graphe.Noeuds.Values)
            {
                double angle = 2 * Math.PI * i / n;
                int x = _centre.X + (int)(_rayon * Math.Cos(angle));
                int y = _centre.Y + (int)(_rayon * Math.Sin(angle));
                noeud.Position = new Point(x, y);
                i++;
            }
        }


        /// <summary>
        /// Dessine le graphe sur le contexte graphique spécifié.
        /// </summary>
        /// <param name="graphics">Le contexte graphique sur lequel dessiner.</param>
        public void Dessiner(Graphics graphics)
        {
            // Dessiner les liens avec leur couleur et largeur
            foreach (var lien in _graphe.Liens)
            {
                using (Pen stylo = new Pen(lien.Couleur, lien.Largeur))
                {
                    graphics.DrawLine(stylo, lien.Noeud1.Position, lien.Noeud2.Position);
                }
            }

            // Dessiner les nœuds
            foreach (var noeud in _graphe.Noeuds.Values)
            {
                Rectangle rectangle = new Rectangle(noeud.Position.X - 5, noeud.Position.Y - 5, 10, 10);
                graphics.FillEllipse(Brushes.Red, rectangle);
                graphics.DrawEllipse(Pens.Black, rectangle);
                graphics.DrawString(noeud.Id.ToString(), SystemFonts.DefaultFont, Brushes.Black, noeud.Position);
            }
        }
    }
}

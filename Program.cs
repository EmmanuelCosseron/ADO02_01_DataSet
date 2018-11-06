using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO02_01_Dataset
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Stagiaires
            List<Personne> liste = new List<Personne>();
            liste.Add(new Personne { Id = 1, Nom = "Angélique", Ville = "Paris" });
            liste.Add(new Personne { Id = 2, Nom = "Ahmed", Ville = "Lille" });
            liste.Add(new Personne { Id = 3, Nom = "Ali", Ville = "Paris" });
            liste.Add(new Personne { Id = 4, Nom = "Clément", Ville = "Marseille" });
            liste.Add(new Personne { Id = 5, Nom = "Angélique", Ville = "Lille" });
            liste.Add(new Personne { Id = 6, Nom = "Amine", Ville = "Lille" });
            liste.Add(new Personne { Id = 7, Nom = "Khalil", Ville = "Paris" });
            liste.Add(new Personne { Id = 8, Nom = "Hacène", Ville = "Lille" });
            liste.Add(new Personne { Id = 9, Nom = "Imen", Ville = "Paris" });
            liste.Add(new Personne { Id = 10, Nom = "Benjamin", Ville = "Marseille" });
            liste.Add(new Personne { Id = 11, Nom = "Emmanuel", Ville = "Marseille" });
            #endregion

            DataSet data = new DataSet();
            // Table Ville
            DataTable tableVille = new DataTable();
            tableVille.TableName = "Ville";
            tableVille.Columns.Add(new DataColumn("Id", typeof(int)));
            tableVille.Columns.Add(new DataColumn("Nom", typeof(string)));
            var listeVille = liste.Select(p => p.Ville).Distinct();
            int compteur = 1;
            foreach (var v in listeVille)
            {
                DataRow row = tableVille.NewRow();
                row["Id"] = compteur;
                row["Nom"] = v;
                tableVille.Rows.Add(row);
                compteur++;
            }
            data.Tables.Add(tableVille);

            DataTable table = new DataTable();
            table.TableName = "Stagiaire";
            table.Columns.Add(new DataColumn("Id", typeof(int)));
            table.Columns.Add(new DataColumn("Nom", typeof(string)));
            table.Columns.Add(new DataColumn("Ville", typeof(int)));
            data.Tables.Add(table);
            foreach (var p in liste)
            {
                DataRow row = table.NewRow();
                var rowVille = tableVille.AsEnumerable()
                    .Where(r => r.Field<string>("Nom") == p.Ville)
                    .FirstOrDefault();
                row["Id"] = p.Id;
                row["Nom"] = p.Nom;
                row["Ville"] = rowVille["Id"];
                table.Rows.Add(row);
            }
        }
    }
    class Personne
    {
        public int Id;
        public string Nom;
        public string Ville;
        public override string ToString()
        {
            return $"{Nom}:{Ville}";
        }
    }
}

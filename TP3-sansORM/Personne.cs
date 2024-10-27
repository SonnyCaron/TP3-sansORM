using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3_sansORM
{
    class Personne
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public DateOnly DateNaissance { get; set; }
        public string? Metier { get; set; }

        public Personne(int id, string? nom, string? prenom, DateOnly dateNaissance, string? metier)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            DateNaissance = dateNaissance;
            Metier = metier;
        }
        public override string ToString()
        {
            return $"Id:{Id}\tNom:{Nom}\tPrenom:{Prenom}\tNaissance:{DateNaissance}\tMetier:{Metier}";
        }
    }
}

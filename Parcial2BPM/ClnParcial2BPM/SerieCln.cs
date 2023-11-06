using CadParcial2BPM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClnParcial2BPM
{
    public class SerieCln
    {
        public static int insertar(Serie serie)
        {
            using (var context = new Parcial2BPMEntities())
            {
                context.Serie.Add(serie);
                context.SaveChanges();
                return serie.id;
            }
        }

        public static int actualizar(Serie serie)
        {
            using (var context = new Parcial2BPMEntities())
            {
                var existente = context.Serie.Find(serie.id);
                existente.titulo = serie.titulo;
                existente.sinopsis = serie.sinopsis;
                existente.director = serie.director;
                existente.duracion = serie.duracion;
                existente.fechaEstreno = serie.fechaEstreno;
                return context.SaveChanges();
            }
        }

        public static int eliminar(int id, string usuarioRegistro)
        {
            using (var context = new Parcial2BPMEntities())
            {
                var existente = context.Serie.Find(id);
                existente.estado = -1;
                return context.SaveChanges();
            }
        }

        public static Serie get(int id)
        {
            using (var context = new Parcial2BPMEntities())
            {
                return context.Serie.Find(id);
            }
        }

        public static List<Serie> listar()
        {
            using (var context = new Parcial2BPMEntities())
            {
                return context.Serie.Where(x => x.estado != -1).ToList();
            }
        }

        public static List<paSerieListar_Result> listarPa(string parametro)
        {
            using (var context = new Parcial2BPMEntities())
            {
                return context.paSerieListar(parametro).ToList();
            }
        }
    }
}

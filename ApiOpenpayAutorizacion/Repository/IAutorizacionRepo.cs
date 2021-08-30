using ApiOpenpayAutorizacion.Models;
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ApiOpenpayAutorizacion.Repository
{
    public interface IAutorizacionRepo
    {
        public int autorizar(AutorizacionRequest auto);
        public bool cancelar(AutorizacionRequest auto);
    }
}

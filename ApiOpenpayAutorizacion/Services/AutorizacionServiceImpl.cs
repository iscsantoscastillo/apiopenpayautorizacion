using ApiOpenpayAutorizacion.Models;
using ApiOpenpayAutorizacion.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiOpenpayAutorizacion.Services
{
    public class AutorizacionServiceImpl : IAutorizacionService
    {
        private IAutorizacionRepo _autorizacionRepo;
        public AutorizacionServiceImpl(IAutorizacionRepo autorizacionRepo) {
            this._autorizacionRepo = autorizacionRepo;
        }
        public int autorizar(AutorizacionRequest auto)
        {
            return _autorizacionRepo.autorizar(auto);
        }

        public bool cancelar(AutorizacionRequest auto) {
            return _autorizacionRepo.cancelar(auto);
        }
    }
}

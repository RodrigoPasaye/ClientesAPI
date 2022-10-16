﻿using ClientesAPI.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientesAPI.Repositorio {
    public interface IClienteRepositorio {
        Task<List<ClienteDto>> GetClientes();
        Task<ClienteDto> GetClienteById(int id);
        Task<ClienteDto> CreateUpdate(ClienteDto clienteDto);
        Task<bool> DeleteCliente(int id);
    }
}

using AutoMapper;
using ClientesAPI.Data;
using ClientesAPI.Models;
using ClientesAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientesAPI.Repositorio {
    public class ClienteRepositorio : IClienteRepositorio {

        private readonly ApplicationDbContext _context;
        private IMapper _mapper;

        public ClienteRepositorio(ApplicationDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClienteDto> CreateUpdate(ClienteDto clienteDto) {
            Cliente cliente = _mapper.Map<ClienteDto, Cliente>(clienteDto);
            if (cliente.Id > 0) {
                _context.Clientes.Update(cliente);
            } else {
                await _context.Clientes.AddAsync(cliente);
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<Cliente, ClienteDto>(cliente);
        }

        public async Task<bool> DeleteCliente(int id) {
            try {
                Cliente cliente = await _context.Clientes.FindAsync(id);
                if (cliente == null) {
                    return false;
                }
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception) {
                return false;
            }
        }

        public async Task<ClienteDto> GetClienteById(int id) {
            Cliente cliente = await _context.Clientes.FindAsync(id);
            return _mapper.Map<ClienteDto>(cliente);
        }

        public async Task<List<ClienteDto>> GetClientes() {
            List<Cliente> clientes = await _context.Clientes.ToListAsync();
            return _mapper.Map<List<ClienteDto>>(clientes);
        }
    }
}

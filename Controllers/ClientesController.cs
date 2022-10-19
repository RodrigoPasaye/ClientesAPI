using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientesAPI.Data;
using ClientesAPI.Models;
using ClientesAPI.Repositorio;
using ClientesAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;

namespace ClientesAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientesController : ControllerBase {
        private readonly IClienteRepositorio _clienteRepositorio;
        protected ResponseDto _response;

        public ClientesController(IClienteRepositorio clienteRepositorio) {
            _clienteRepositorio = clienteRepositorio;
            _response = new ResponseDto();
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes() {
            try {
                var clientes = await _clienteRepositorio.GetClientes();
                _response.Result = clientes;
                _response.DisplayMessage = "Lista de Clientes";
                return Ok(_response);
            } catch (Exception ex) {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al obtener los Clientes";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id) {
            var cliente = await _clienteRepositorio.GetClienteById(id);

            if (cliente == null) {
                _response.IsSuccess = false;
                _response.DisplayMessage = "El Cliente no Existe";
                return NotFound(_response);
            }

            _response.Result = cliente;
            _response.DisplayMessage = "Información del Cliente";
            return Ok(_response);
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(ClienteDto clienteDto) {
            try {
                ClienteDto cliente = await _clienteRepositorio.CreateUpdate(clienteDto);
                _response.Result = cliente;
                _response.DisplayMessage = "Se Actualizó el Cliente con Éxito";
                return Ok(_response);
            }
            catch (Exception ex) {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al actualizar el Cliente";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(ClienteDto clienteDto) {
            try {
                ClienteDto cliente = await _clienteRepositorio.CreateUpdate(clienteDto);
                _response.Result = cliente;
                _response.DisplayMessage = "Se Guardo el Cliente con Éxito";
                return CreatedAtAction("GetCliente", new { id = cliente.Id }, _response);
            } catch (Exception ex) {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al guardar el Cliente";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id) {
            try {
                bool seElimino = await _clienteRepositorio.DeleteCliente(id);
                if (seElimino) {
                    _response.Result = seElimino;
                    _response.DisplayMessage = "Cliente Eliminado con Éxito";
                    return Ok(_response);
                } else {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Error al Eliminar Cliente";
                    return BadRequest(_response);
                }
            } catch (Exception ex) {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al Eliminar el Cliente";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }
    }
}

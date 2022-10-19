using ClientesAPI.Models;
using ClientesAPI.Models.Dto;
using ClientesAPI.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClientesAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly IUserRepositorio _userRepositorio;
        protected ResponseDto _responseDto;
        public UsersController(IUserRepositorio userRepositorio) {
            _userRepositorio = userRepositorio;
            _responseDto = new ResponseDto();
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserDto userDto) {
            var respuesta = await _userRepositorio.Register(new User {
                UserName = userDto.UserName
            }, userDto.Password);
            if (respuesta == -1) {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "El Usuario ya Existe";
                return BadRequest(_responseDto);
            }
            if (respuesta == -500) {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "Error al Crear el Usuario";
                return BadRequest(_responseDto);
            }
            _responseDto.DisplayMessage = "Usuario Creado con Éxito";
            _responseDto.Result = respuesta;
            return Ok(_responseDto);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserDto userDto) {
            var respuesta = await _userRepositorio.Login(userDto.UserName, userDto.Password);
            if (respuesta == "noUser") {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "Usuario No Existe";
                return BadRequest(_responseDto);
            }
            if (respuesta == "wrongPassword") {
                _responseDto.IsSuccess = false;
                _responseDto.DisplayMessage = "Contraseña Incorrecta";
                return BadRequest(_responseDto);
            }
            return Ok("Usuario Correcto");
        }
    }
}

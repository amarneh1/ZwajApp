using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZwajApp.API.Data;
using ZwajApp.API.Dtos;

namespace ZwajApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IZwajRepository _repo;
        public IMapper _Mapper { get; }
        public UsersController(IZwajRepository repo,IMapper mapper)
        {
            _Mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(){
            var users = await _repo.GetUsers();
            var usersToReturn = _Mapper.Map<IEnumerable<UserForListDto>>(users);
            return Ok(usersToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Getuser(int id){
            var user = await _repo.GetUser(id);
            var userToReturn = _Mapper.Map<UserForDetailsDto>(user);
            return Ok(userToReturn);
        }

        
    }
}
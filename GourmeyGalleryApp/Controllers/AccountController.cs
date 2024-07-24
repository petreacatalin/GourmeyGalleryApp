using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using GourmeyGalleryApp.Models.DTOs;
using GourmeyGalleryApp.Models.Entities;
using GourmeyGalleryApp.Services.UserService.UserService;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
   

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration,
        IUserService userService,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = new ApplicationUser
        {
            FirstName = registerDto.FirstName, 
            LastName = registerDto.LastName,
            UserName = registerDto.Email,
            Email = registerDto.Email,
            ProfilePictureUrl = registerDto.ProfilePictureUrl ?? "https://cdn.icon-icons.com/icons2/1378/PNG/512/avatardefault_92824.png"
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.UserName);
        if (user == null)
        {
            return Unauthorized("Invalid login attempt.");
        }

        var result = await _userManager.CheckPasswordAsync(user,loginDto.Password);
        if (!result)
        {
            return Unauthorized("Invalid login attempt.");
        }

        var token = GenerateJwtToken(user);
        return Ok(new AuthResult()
        {
            Token = token,
            Result = true,
           
        });
    }

    private string GenerateJwtToken(ApplicationUser user)
    {

        var tokenHandler = new JwtSecurityTokenHandler();
        var key  = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtConfig").GetSection("Key").Value!);

        List<Claim> claims =
            [
                new(JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new(JwtRegisteredClaimNames.FamilyName, user.FirstName ?? ""),
                new(JwtRegisteredClaimNames.GivenName , user.LastName ?? ""),
                new(JwtRegisteredClaimNames.Name, user.UserName ?? ""),
                new(JwtRegisteredClaimNames.NameId, user.Id ?? ""),
                new(JwtRegisteredClaimNames.Aud, 
                _configuration.GetSection("JwtConfig").GetSection("Audience").Value!),
                new(JwtRegisteredClaimNames.Iss,
                _configuration.GetSection("JwtConfig").GetSection("Issuer").Value!),

            ];

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);

    //    var claims = new[]
    //    {
    //    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
    //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    //    new Claim(JwtRegisteredClaimNames.NameId, user.Id) // Ensure user ID is included
    //};

    //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]));
    //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //    var token = new JwtSecurityToken(
    //        issuer: _configuration["JwtConfig:Issuer"],
    //        audience: _configuration["JwtConfig:Audience"],
    //        claims: claims,
    //        expires: DateTime.UtcNow.AddDays(14), // Token expiration
    //        signingCredentials: creds);

    //    return new JwtSecurityTokenHandler().WriteToken(token);
    }


    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = User.FindFirstValue("nameId");
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var user = await _userService.GetUserByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var userDto = _mapper.Map<ApplicationUserDto>(user);
        return Ok(userDto);
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile(ApplicationUserDto userDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userService.GetUserByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        user.ProfilePictureUrl = userDto.ProfilePictureUrl;

        await _userService.UpdateUserAsync(user);

        return NoContent();
    }

    [HttpGet("friends")]
    public async Task<IActionResult> GetFriends()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var friends = await _userService.GetFriendsAsync(userId);

        if (friends == null)
        {
            return NotFound();
        }

        var friendDtos = _mapper.Map<IEnumerable<ApplicationUserDto>>(friends);
        return Ok(friendDtos);
    }

    [HttpPost("add-friend/{friendId}")]
    public async Task<IActionResult> AddFriend(string friendId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _userService.AddFriendAsync(userId, friendId);

        return Ok();
    }

    [HttpPost("accept-friend/{friendId}")]
    public async Task<IActionResult> AcceptFriend(string friendId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _userService.AcceptFriendAsync(userId, friendId);

        return Ok();
    }
}


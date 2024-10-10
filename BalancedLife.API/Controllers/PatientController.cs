using BalancedLife.Application.DTOs.User;
using BalancedLife.Application.Interfaces;
using BalancedLife.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BalancedLife.API.Controllers {
    [Route("api")]
    [ApiController]
    public class PatientController : ControllerBase {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService) {
            _patientService = patientService;
        }

        [Authorize]
        [HttpGet("user/patients")]
        public async Task<IActionResult> GetPatients(
                [FromQuery] int page = 1,
                [FromQuery] int pageSize = 25,
                [FromQuery] string? patientName = null,
                [FromQuery] string? status = null
            ) {
            try {
                StatusNutritionist? parsedStatus = null;
                if ( !string.IsNullOrEmpty(status) && Enum.TryParse<StatusNutritionist>(status, true, out var statusEnum) ) {
                    parsedStatus = statusEnum;
                }
                var userId = User.FindFirstValue(JwtRegisteredClaimNames.Jti);
                var result = await _patientService.GetPatients(long.Parse(userId), page, pageSize, patientName, parsedStatus);
                return Ok(new {
                    patients = result,
                    quantityPages = await _patientService.GetNumberPagePatients(long.Parse(userId), pageSize, patientName, parsedStatus)
                });
        } catch (Exception ex ) {
                return BadRequest(new { message = $"{ex.Message}" });
            }
        }

        [Authorize]
[HttpDelete("user/patient/{id}")]
public async Task<IActionResult> DeletePatient(long id) {
    try {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Jti);
        await _patientService.DeletePatient(id, long.Parse(userId));

        return Ok();
    } catch ( Exception ex ) {
        return BadRequest(new { message = $"{ex.Message}" });
    }
}

[Authorize]
[HttpPost("user/patient")]
public async Task<IActionResult> AddPatient([FromBody] PatientLinkDTO patient) {
    try {
        var result = await _patientService.AddPatient(patient);
        if ( result != null ) {
            return CreatedAtAction(nameof(AddPatient), result);
        }

        return BadRequest(new { message = "Não foi possível registrar o paciente, por favor verifique os dados!" });
    } catch ( Exception ex ) {
        return BadRequest(new { message = $"{ex.Message}" });
    }
}

[Authorize]
[HttpPut("user/patient")]
public async Task<IActionResult> UpdatePatient([FromBody] PatientLinkDTO patient) {
    try {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Jti);
        if ( patient.IdNutritionist != long.Parse(userId) && patient.IdPatient != long.Parse(userId) )
            return Unauthorized(new { message = "Você não tem permissão para atualizar este paciente." });

        var result = await _patientService.UpdatePatient(patient);
        if ( result == null ) {
            return NotFound(new { message = "Paciente não encontrado." });
        }

        return Ok(result);
    } catch ( Exception ex ) {
        return BadRequest(new { message = $"{ex.Message}" });
    }
}

[Authorize]
[HttpGet("user/patient/{id}/validate")]
public async Task<IActionResult> IsYourPatient(long id) {
    try {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Jti);
        var result = await _patientService.IsYourPatient(long.Parse(userId), id);
        return Ok(result);
    } catch ( Exception ex ) {
        return BadRequest(new { message = $"{ex.Message}" });
    }
}

[Authorize]
[HttpGet("user/patient/link/{id}")]
public async Task<IActionResult> GetPatientLinkById(long id) {
    try {
        var result = await _patientService.GetPatientLinkById(id);
        if ( result == null ) {
            return NotFound(new { message = "Paciente não encontrado." });
        }

        return Ok(result);
    } catch ( Exception ex ) {
        return BadRequest(new { message = $"{ex.Message}" });
    }
}

[Authorize]
[HttpGet("user/nutritionists")]
public async Task<IActionResult> GetNutritionistsByPatient() {
    try {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

        if ( string.IsNullOrEmpty(userId) )
            return Unauthorized(new { message = "Usuário não autorizado." });

        var result = await _patientService.GetNutritionistsByPatientId(long.Parse(userId));
        return Ok(result);
    } catch ( Exception ex ) {
        return BadRequest(new { message = $"{ex.Message}" });
    }
}

[Authorize]
[HttpGet("user/nutritionist")]
public async Task<IActionResult> GetActualNutritionist() {
    try {
        var userId = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

        if ( string.IsNullOrEmpty(userId) )
            return Unauthorized(new { message = "Usuário não autorizado." });

        var result = await _patientService.GetActualNutritionist(long.Parse(userId));
        return Ok(result);
    } catch ( Exception ex ) {
        return BadRequest(new { message = $"{ex.Message}" });
    }
}
    }
}

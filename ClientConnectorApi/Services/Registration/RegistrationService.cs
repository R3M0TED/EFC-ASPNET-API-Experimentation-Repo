using ClientConnectorApi.Dtos.Requests;
using ClientConnectorApi.Dtos.Responses;
using ClientConnectorApi.Exceptions;
using ClientConnectorApi.Services.Factories;
using MessaingData;
using Microsoft.EntityFrameworkCore;

namespace ClientConnectorApi.Services.Registration
{
    public class RegistrationService : IRegistrationService
    {
        private readonly MessagingContext _context;

        public RegistrationService(MessagingContext context)
        {
            _context = context;
        }

        public async Task<UserDTO> RegisterUserAsync(UserRegistrationDTO registerationDto)
        {

            if (await _context.Users.FirstOrDefaultAsync(x => x.Username == registerationDto.Username) == null)
            {
                var userEntity = UserFactory.DtoToEntity(registerationDto);
                _context.Users.Add(userEntity);
                await _context.SaveChangesAsync();

                return UserFactory.EntityToDto(userEntity);
            }

            throw new DuplicateDataException("A user already exists with that username.");
        }
    }
}

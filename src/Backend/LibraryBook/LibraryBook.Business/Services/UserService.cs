using LibraryBook.Business.Entities;
using LibraryBook.Business.Interface;
using LibraryBook.Business.Interface.Repository;
using LibraryBook.Business.Interface.Service;
using LibraryBook.Business.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBook.Business.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(INotificador notificador, IUserRepository userRepository) : base(notificador)
        {
            _userRepository = userRepository;
        }

        public async Task Add(User user)
        {
            if (!ExecutarValidacao(new UserValidation(), user)) return;

            await _userRepository.Add(user);
        }

        public Task Update(User user)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _userRepository.Dispose();
        }

        
    }
}

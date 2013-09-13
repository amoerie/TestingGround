using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestingGround.Core.Domain.Internal.Bases;

namespace TestingGround.Core.Domain.Security
{
    public class UserProfile: Entity
    {
        public string UserName { get; set; }
    }
}
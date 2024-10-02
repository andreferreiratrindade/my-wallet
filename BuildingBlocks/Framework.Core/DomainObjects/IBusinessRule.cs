using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Core.DomainObjects
{
    public interface IBusinessRule
    {
        Task<bool> IsBroken();

        List<string> Message { get; }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLACalc.Application.SLA.Queries
{
    public class GetPrioritiesQuery : IRequest<List<string>>
    {
    }
}

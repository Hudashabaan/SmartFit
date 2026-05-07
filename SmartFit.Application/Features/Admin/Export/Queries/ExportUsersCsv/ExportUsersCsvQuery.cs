using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace SmartFit.Application.Features.Admin.Export.Queries.ExportUsersCsv
{
    public class ExportUsersCsvQuery : IRequest<byte[]>
    {
    }
}

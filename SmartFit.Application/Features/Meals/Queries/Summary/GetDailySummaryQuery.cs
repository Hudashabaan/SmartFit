using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SmartFit.Application.Features.Meals.DTOs;

namespace SmartFit.Application.Features.Meals.Queries.Summary
{
  

    public class GetDailySummaryQuery : IRequest<DailySummaryDto>
    {
        public DateTime Date { get; set; }

        public GetDailySummaryQuery(DateTime date)
        {
            Date = date;
        }
    }
}

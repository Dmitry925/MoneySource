﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MoneySource.Core.Application.Infrastructure.Exceptions;
using MoneySource.Core.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace MoneySource.Core.Application.Features.SourceFeatures.Commands
{
    public class PutSourceCommand
    {
        public class Request : IRequest<Response>
        {
            [JsonIgnore]
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public string Name { get; set; }

        public class PutSourceCommandHandler : IRequestHandler<Request, Response>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public PutSourceCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var source = await _context.Sources.Where(a => a.Id == request.Id).FirstOrDefaultAsync();

                if (source == null)
                {
                    throw new NotFoundException(nameof(source));
                }

                _mapper.Map(request, source);
                await _context.SaveAsync();

                return new Response
                {
                    Id = source.Id
                };
            }
        }

        public class Response
        {
            public Guid Id { get; set; }
        }
    }
}

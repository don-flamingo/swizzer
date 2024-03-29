﻿using Swizzer.Shared.Common.Domain.Messages.Commands;
using Swizzer.Web.Infrastructure.Cqrs.Commands;
using Swizzer.Web.Infrastructure.Domain.Messages.Models;
using Swizzer.Web.Infrastructure.Mappers;
using Swizzer.Web.Infrastructure.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swizzer.Web.Infrastructure.Domain.Messages.Commands
{
    public class MessageCommandHandler : ICommandHandler<CreateMessageCommand>
    {
        private readonly ISwizzerMapper _swizzerMapper;
        private readonly SwizzerContext _context;

        public MessageCommandHandler(
            ISwizzerMapper swizzerMapper,
            SwizzerContext context)
        {
            this._swizzerMapper = swizzerMapper;
            this._context = context;
        }
        public async Task HandleAsync(CreateMessageCommand command)
        {
            var message = _swizzerMapper.MapTo<Message>(command);
            
            message.ReceiverId = command.Receiver;
            message.SenderId = command.RequestBy;

            await  _context.AddAsync(message);
        }
    }
}

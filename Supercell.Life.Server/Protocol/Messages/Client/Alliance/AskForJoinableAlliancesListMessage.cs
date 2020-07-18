﻿namespace Supercell.Life.Server.Protocol.Messages.Client
{
    using Supercell.Life.Titan.DataStream;

    using Supercell.Life.Server.Network;
    using Supercell.Life.Server.Protocol.Enums;
    using Supercell.Life.Server.Protocol.Messages.Server;

    internal class AskForJoinableAlliancesListMessage : PiranhaMessage
    {
        /// <summary>
        /// The service node for this message.
        /// </summary>
        internal override ServiceNode Node
        {
            get
            {
                return ServiceNode.Alliance;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AskForJoinableAlliancesListMessage"/> class.
        /// </summary>
        public AskForJoinableAlliancesListMessage(Connection connection, ByteStream stream) : base(connection, stream)
        {
            // AskForJoinableAlliancesListMessage.
        }

        internal override void Handle()
        {
            new JoinableAllianceListMessage(this.Connection).Send();
        }
    }
}
﻿namespace Supercell.Life.Server.Protocol.Messages.Server
{
    using Supercell.Life.Server.Logic.Alliance.Streams;
    using Supercell.Life.Server.Network;
    using Supercell.Life.Server.Protocol.Enums;

    internal class AllianceStreamMessage : PiranhaMessage
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
        /// Initializes a new instance of the <see cref="AllianceStreamMessage"/> class.
        /// </summary>
        internal AllianceStreamMessage(Connection connection) : base(connection)
        {
            this.Type = Message.AllianceStream;
        }

        internal override void Encode()
        {
            this.Stream.WriteInt(this.Connection.Avatar.Alliance.Entries.Count);

            foreach (StreamEntry entry in this.Connection.Avatar.Alliance.Entries)
            {
                entry.Encode(this.Stream);
            }
        }
    }
}

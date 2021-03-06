﻿namespace Supercell.Life.Server.Logic.Avatar.Timers
{
    using Newtonsoft.Json;

    using Supercell.Life.Titan.Logic.Json;
    using Supercell.Life.Titan.Logic.Math;

    using Supercell.Life.Server.Files.CsvLogic;
    using Supercell.Life.Server.Helpers;

    internal class LogicBoosterTimer
    {
        internal LogicClientAvatar Avatar;

        internal LogicBoosterData BoostPackage;

        [JsonProperty("boost_package")] internal int BoosterID;
        [JsonProperty("timer")]         internal LogicTimer Timer;

        /// <summary>
        /// Gets a value indicating whether this <see cref="LogicBoosterTimer"/> has started.
        /// </summary>
        internal bool BoostActive
        {
            get
            {
                return this.Timer.Started && this.BoosterID != -1;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogicBoosterTimer"/> class.
        /// </summary>
        public LogicBoosterTimer()
        {
            this.Timer = new LogicTimer();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogicBoosterTimer"/> class.
        /// </summary>
        public LogicBoosterTimer(LogicClientAvatar avatar)
        {
            this.Avatar = avatar;
            this.Timer  = new LogicTimer(avatar.Time);
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        internal void Start()
        {
            this.BoosterID = this.BoostPackage.GlobalID;
            this.Timer.StartTimer(this.Avatar.Time, this.BoostPackage.TimeDays * 86400);
        }

        /// <summary>
        /// Finishes this instance.
        /// </summary>
        internal void Finish()
        {
            if (this.BoostActive)
            {
                this.Timer.StopTimer();

                this.BoostPackage = null;
                this.BoosterID    = -1;

                this.Avatar.Save();
            }
        }

        /// <summary>
        /// Fast forwards this instance by the specified number of seconds.
        /// </summary>
        internal void FastForward(int seconds)
        {
            if (this.BoostActive)
            {
                this.Timer.FastForward(seconds);

                if (this.Timer.RemainingSecs <= 0)
                {
                    this.Finish();
                }
            }
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        internal void Tick()
        {
            if (this.Timer.Started)
            {
                if (this.Timer.RemainingSecs <= 0)
                {
                    this.Finish();
                }
            }
        }

        /// <summary>
        /// Adjusts the subtick of this instance.
        /// </summary>
        internal void AdjustSubTick()
        {
            if (this.BoostActive)
            {
                this.Timer.AdjustSubTick();
            }
        }

        /// <summary>
        /// Saves this instance to the specified <see cref="LogicJSONObject"/>.
        /// </summary>
        internal void Save(LogicJSONObject json)
        {
            if (this.BoosterID != -1)
            {
                json.Put("xp_boost_t", new LogicJSONNumber(this.Timer.RemainingSecs));
                json.Put("xp_boost_p", new LogicJSONNumber(this.BoosterID));
            }
        }
    }
}
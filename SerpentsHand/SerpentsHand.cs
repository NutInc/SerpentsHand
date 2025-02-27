using Exiled.API.Features;
using HarmonyLib;
using SerpentsHand.Events;
using System;
using Player = Exiled.Events.Handlers.Player;
using Scp106 = Exiled.Events.Handlers.Scp106;
using Server = Exiled.Events.Handlers.Server;
using Warhead = Exiled.Events.Handlers.Warhead;

namespace SerpentsHand
{
    public class SerpentsHand : Plugin<Config>
    {
        public static SerpentsHand Singleton;

        public override string Name => "Serpents Hand";
        public override string Author => "yanox, Michal78900 and Marco15453";
        public override Version RequiredExiledVersion => new Version(5, 3, 0);
        public override Version Version => new Version(4, 5, 1);

        public int TeamRespawnCount;
        public int SerpentsRespawnCount;
        public bool IsSpawnable;

        private PlayerHandler playerHandler;
        private ServerHandler serverHandler;
        private WarheadHandler warheadHandler;

        private Harmony harmony;

        public override void OnEnabled()
        {
            Singleton = this;

            harmony = new Harmony($"marco15453.serpentshand-{DateTime.Now.Ticks}");
            harmony.PatchAll();

            RegisterEvents();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            harmony.UnpatchAll();

            UnregisterEvents();
            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            playerHandler = new PlayerHandler();
            serverHandler = new ServerHandler();
            warheadHandler = new WarheadHandler();

            // Player
            Player.FailingEscapePocketDimension += playerHandler.OnFailingEscapePocketDimension;
            Player.EscapingPocketDimension += playerHandler.OnEscapingPocketDimension;
            Player.Hurting += playerHandler.OnHurting;
            Player.Shooting += playerHandler.OnShooting;
            Player.ActivatingGenerator += playerHandler.OnActivatingGenerator;
            Player.Destroying += playerHandler.OnDestroying;
            Player.Died += playerHandler.OnDied;
            Player.ChangingRole += playerHandler.OnChangingRole;
            Player.SpawningRagdoll += playerHandler.OnSpawningRagdoll;
            Player.EnteringPocketDimension += playerHandler.OnEnteringPocketDimension;

            // Server
            Server.WaitingForPlayers += serverHandler.OnWaitingForPlayers;
            Server.RespawningTeam += serverHandler.OnRespawningTeam;
            Server.EndingRound += serverHandler.OnEndingRound;

            // Warhead
            Warhead.Detonated += warheadHandler.OnDetonated;
        }

        private void UnregisterEvents()
        {
            // Player
            Player.FailingEscapePocketDimension -= playerHandler.OnFailingEscapePocketDimension;
            Player.EscapingPocketDimension -= playerHandler.OnEscapingPocketDimension;
            Player.Hurting -= playerHandler.OnHurting;
            Player.Shooting -= playerHandler.OnShooting;
            Player.ActivatingGenerator -= playerHandler.OnActivatingGenerator;
            Player.Destroying -= playerHandler.OnDestroying;
            Player.Died -= playerHandler.OnDied;
            Player.ChangingRole -= playerHandler.OnChangingRole;
            Player.SpawningRagdoll -= playerHandler.OnSpawningRagdoll;
            Player.EnteringPocketDimension -= playerHandler.OnEnteringPocketDimension;

            // Server
            Server.WaitingForPlayers -= serverHandler.OnWaitingForPlayers;
            Server.RespawningTeam -= serverHandler.OnRespawningTeam;
            Server.EndingRound -= serverHandler.OnEndingRound;

            // Warhead
            Warhead.Detonated -= warheadHandler.OnDetonated;
            
            playerHandler = null;
            serverHandler = null;
            warheadHandler = null;
        }
    }
}

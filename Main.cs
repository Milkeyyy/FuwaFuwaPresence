using DiscordRPC;
using DiscordRPC.Message;
using Produire;
using System;

namespace FuwaFuwaPresence
{
	public class DiscordRPCクライアント : IProduireClass
	{
		private DiscordRpcClient client;
		private RichPresence presence;
		private Timestamps timestamps;
		private Assets assets;

		public DiscordRPCクライアント(string アプリケーションID)
		{
			// RPCクライアント
			client = new DiscordRpcClient(アプリケーションID);
			// イベント
			client.OnReady += Client_OnReady;
			client.OnClose += Client_OnClose;
			client.OnPresenceUpdate += Client_OnPresenceUpdate;
			client.OnConnectionFailed += Client_OnConnectionFailed;
			// RPC情報
			presence = new RichPresence();
			// 日時情報
			timestamps = new Timestamps();
			presence.Timestamps = timestamps;
			// アセット情報
			assets = new Assets();
			presence.Assets = assets;
		}

		/// <summary>
		/// Discord クライアントがメッセージを送受信する準備ができた時のイベント
		/// </summary>
		public event EventHandler 準備が完了した;
		private void Client_OnReady(object sender, object e)
		{
			準備が完了した?.Invoke(sender, EventArgs.Empty);
		}
		/// <summary>
		/// Discord クライアントへの接続が失われた時のイベント
		/// </summary>
		public event EventHandler 接続が閉じられた;
		private void Client_OnClose(object sender, object e)
		{
			接続が閉じられた?.Invoke(sender, EventArgs.Empty);
		}
		/// <summary>
		/// Discord クライアントがプレゼンスを更新した時のイベント
		/// </summary>
		public event EventHandler ステータスが更新された;
		private void Client_OnPresenceUpdate(object sender, object e)
		{
			ステータスが更新された?.Invoke(sender, EventArgs.Empty);
		}
		/// <summary>
		/// Discord クライアントとの接続を確立できなかった時のイベント
		/// </summary>
		public event EventHandler 接続が失敗した;
		private void Client_OnConnectionFailed(object sender, ConnectionFailedMessage args)
		{
			接続が失敗した?.Invoke(sender, new 接続が失敗した情報(args));
		}

		public class 接続が失敗した情報 : EventArgs, IProduireClass
		{
			private readonly ConnectionFailedMessage message;

			public 接続が失敗した情報(ConnectionFailedMessage args)
			{
				message = args;
			}

			/// <summary>
			/// パイプの接続に失敗したかどうか
			/// </summary>
			public int パイプ接続失敗
			{
				get { return message.FailedPipe; }
			}
			/// <summary>
			/// メッセージの種類
			/// </summary>
			public MessageType メッセージタイプ
			{
				get { return message.Type; }
			}
		}

		/// <summary>
		/// Discord IPC への接続を初期化しようとする
		/// </summary>
		[自分を]
		public void 初期化()
		{
			client.Initialize();
		}
		/// <summary>
		/// Discord への接続を終了し、オブジェクトを破棄する
		/// </summary>
		[自分を]
		public void 破棄()
		{
			client.ClearPresence();
			client.Dispose();
		}
		/// <summary>
		/// リッチプレゼンスをクリアする
		/// </summary>
		[自分を]
		public void クリア()
		{
			client.ClearPresence();
		}
		/// <summary>
		/// リッチプレゼンスを更新する
		/// </summary>
		[自分を]
		public void 更新()
		{
			client.SetPresence(presence);
		}

		/// <summary>
		/// アプリケーションのID
		/// </summary>
		public string アプリケーションID
		{
			get { return client.ApplicationID; }
		}
		/// <summary>
		/// アクティビティの詳細
		/// (1行目に表示されるテキスト)
		/// </summary>
		public string 詳細
		{
			get { return presence.Details; }
			set { presence.Details = value; }
		}
		/// <summary>
		/// アクティビティの状態
		/// (2行目に表示されるテキスト)
		/// </summary>
		public string 状態
		{
			get { return presence.State; }
			set { presence.State = value; }
		}
		/// <summary>
		/// アクティビティの種類
		/// </summary>
		public ActivityType アクティビティタイプ
		{
			get { return presence.Type; }
			set { presence.Type = value; }
		}
		/// <summary>
		/// アクティビティの開始日時
		/// </summary>
		public object 開始日時
		{
			get
			{
				if (presence.Timestamps.Start.HasValue) { return ((DateTime)presence.Timestamps.Start).ToLocalTime(); }
				else { return null; }
			}
			set
			{
				if (value == null) { presence.Timestamps.Start = null; }
				else { presence.Timestamps.Start = ((DateTime)value).ToUniversalTime(); }
			}
		}
		/// <summary>
		/// アクティビティの終了日時
		/// </summary>
		public object 終了日時
		{
			get
			{
				if (presence.Timestamps.End.HasValue) { return ((DateTime)presence.Timestamps.End).ToLocalTime(); }
				else { return null; }
			}
			set
			{
				if (value == null) { presence.Timestamps.End = null; }
				else { presence.Timestamps.End = ((DateTime)value).ToUniversalTime(); }
			}
		}
		/// <summary>
		/// アクティビティの画像のキー
		/// </summary>
		public string 大画像キー
		{
			get { return presence.Assets.LargeImageKey; }
			set { presence.Assets.LargeImageKey = value; }
		}
		/// <summary>
		/// 画像にカーソルを合わせた時に表示されるテキスト
		/// </summary>
		public string 大画像テキスト
		{
			get { return presence.Assets.LargeImageText; }
			set { presence.Assets.LargeImageText = value; }
		}
		/// <summary>
		/// アクティビティの画像の右下に表示される小さい画像のキー
		/// </summary>
		public string 小画像キー
		{
			get { return presence.Assets.SmallImageKey; }
			set { presence.Assets.SmallImageKey = value; }
		}
		/// <summary>
		/// 小さい画像にカーソルを合わせた時に表示されるテキスト
		/// </summary>
		public string 小画像テキスト
		{
			get { return presence.Assets.SmallImageText; }
			set { presence.Assets.SmallImageText = value; }
		}
	}

	[列挙体(typeof(MessageType))]
	public enum DiscordRPCメッセージタイプ
	{
		接続終了 = MessageType.Close,
		接続確立 = MessageType.ConnectionEstablished,
		接続失敗 = MessageType.ConnectionFailed,
		エラー = MessageType.Error,
		参加 = MessageType.Join,
		参加要求 = MessageType.JoinRequest,
		プレゼンス更新 = MessageType.PresenceUpdate,
		準備完了 = MessageType.Ready,
		観戦 = MessageType.Spectate,
		登録 = MessageType.Subscribe,
		登録解除 = MessageType.Unsubscribe
	}

	[列挙体(typeof(ActivityType))]
	public enum DiscordRPCアクティビティタイプ
	{
		プレイ中 = ActivityType.Playing,
		再生中 = ActivityType.Listening,
		視聴中 = ActivityType.Watching,
		参戦中 = ActivityType.Competing
	}
}

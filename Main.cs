using DiscordRPC;
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
}

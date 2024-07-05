# FuwaFuwaPresence
プロデルで [Discord Rich Presence](https://discord.com/developers/docs/rich-presence/how-to) ([discord-rpc-csharp](https://github.com/Lachee/discord-rpc-csharp)) の機能を利用できるようにするプラグインです。

## 概要

リッチプレゼンスステータスを表示させるには、[Discord Developer Portal](https://discord.com/developers/applications) でアプリケーションを作成する必要があります。<br>

アプリケーションの作成が完了したら、`DiscordRPCクライアント` を作成する際の初期値へ作成したアプリケーションのIDを指定します。

- [**サンプルコード**](#サンプルコード)
- [**表示内容と設定項目についての詳しい説明**](#表示内容と設定項目について)


## ドキュメント

### `DiscordRPCクライアント`

  ### 作成する方法
  ```
  【名前】というDiscordRPCクライアント(【アプリケーションID】)を作る
  ```


  ### 手順

  #### `【自分】を初期化`
  Discord への接続を初期化します。

  #### `【自分】を破棄`
  Discord への接続を終了し、オブジェクトを破棄します。

  #### `【自分】をクリア`
  リッチプレゼンスをクリアします。

  #### `【自分】を更新`
  リッチプレゼンスを更新します。


  ### 設定項目

  | 設定項目 | 型 |  | 説明 |
  | - | - | - | - |
  | **アプリケーションID** | **文字列** | □ | アプリケーションのID |
  | **詳細** | **文字列** | ◎ | アクティビティの詳細 (1行目に表示されるテキスト) |
  | **状態** | **文字列** | ◎ | アクティビティの状態 (2行目に表示されるテキスト) |
  | **開始日時** | **日時形式** | ◎ | アクティビティの開始日時 |
  | **終了日時** | **日時形式** | ◎ | アクティビティの終了日時 |
  | **大画像キー** | **文字列** | ◎ | アクティビティの大きい画像 (アイコン) のキー |
  | **大画像テキスト** | **文字列** | ◎ | 大きい画像にカーソルを合わせた時に表示されるテキスト |
  | **小画像キー** | **文字列** | ◎ | アクティビティの画像 (アイコン) の右下に表示される小さい画像のキー |
  | **小画像テキスト** | **文字列** | ◎ | 小さい画像にカーソルを合わせた時に表示されるテキスト |


  ### イベント手順

  | イベント名 | 説明 | 情報 |
  | - | - | - |
  | **準備が完了した** | Discord クライアントがメッセージを送受信する準備ができた時 |  |
  | **接続が閉じられた** | Discord クライアントへの接続が失われた時 |  |
  | **ステータスが更新された** | Discord クライアントがプレゼンスを更新した時 |  |


  ### サンプルコード

  #### Discord へリッチプレゼンスステータスを表示

  DiscordRPCクライアントを作成して Discord へリッチプレゼンスステータスを表示し、5秒後にステータスを削除します。

  ```
「FuwaFuwaPresence.dll」を利用する

RPCクライアントというDiscordRPCクライアント(「0000000000000000000」)を作る
    それを初期化する
    それの状態=「State」
    それの詳細=「Details」
    それの開始日時=今
    それの終了日時=無
    それの大画像キー=「Key」
    それの大画像テキスト=「Text」
    それを更新する

5秒待つ

RPCクライアントを破棄する
  ```


### 表示内容と設定項目について

#### 表示される名前

ステータスの「○○○をプレイ中」の「○○○」には、[Discord Developer Portal](https://discord.com/developers/applications) で作成したアプリケーションの名前が表示されます。

これをRPCクライアント側の設定項目から変更することはできません。

![image](https://github.com/Milkeyyy/FuwaFuwaPresence/assets/59532514/fabcd985-0792-4b81-bd97-c8c6c820b6fc)

---

#### 開始日時と終了日時

開始日時と終了日時は、設定する内容によってステータスのタイムスタンプの表記が変わります。

- **両方に無を設定した場合 (何も設定しない場合)**

  タイムスタンプは表示されません。

  ![image](https://github.com/Milkeyyy/FuwaFuwaPresence/assets/59532514/9e5a469b-4be9-4ed5-9dea-826e80ac08c2)

- **開始日時のみ設定した場合**

  設定した開始日時から現在時刻まで**経過時間**が表示されます。

  ![image](https://github.com/Milkeyyy/FuwaFuwaPresence/assets/59532514/6f0e0fa6-eccf-49df-adee-3783ae0a5831)

- **終了日時のみ設定した場合**

  現在時刻から終了日時までの**残り時間**が表示されます。

  ![image](https://github.com/Milkeyyy/FuwaFuwaPresence/assets/59532514/490fca2b-d6e6-44fe-bed4-91fa178f772f)

---

#### 画像 (アイコン)

表示する画像 (アイコン) は、[Discord Developer Portal](https://discord.com/developers/applications) で作成したアプリケーションの設定からアップロードしておく必要があります。

実際にステータスへ画像を設定する際は、設定項目の `大画像キー` または `小画像キー` へ、画像をアップロードした際に指定したキーを設定します。

![image](https://github.com/Milkeyyy/FuwaFuwaPresence/assets/59532514/ba13a0cd-e134-43a4-aae4-edda144af4f1)


## ライセンス

[MIT License](./LICENSE)

Copyright (C) 2024 Milkeyyy

# fighting-game
スマブラのようなアクションゲームの作成
## 制作の目的、プレイヤー、敵の動き方 (基本アクション)
**パワポ載せる**
## メインシーン
**画像をはる**
## ディレクトリ構成 (Assets以下について)
```
└── Assets
    ├── Scenes : ゲームのシーン
    |
    |
    ├── Prefab : (自分的にここ？) プレハブ
    └── Scripts : 各Scripts(C#)を保存
        ├── Behaviour : 継承用スクリプト
        |    ├── PlayerBehaviour : プレイヤーの基本動作
        |    └── EnemyBehaviour : 敵の基本動作
        |
        ├── Effect : エフェクトプレハブへ適用するエフェクトスクリプト
        |    └── PlayerJumpEffect : 一例、ジャンプエフェクト
        |
        ├── Manager : 管理スクリプト
        |     └── EffectManager : 一例、エフェクトマネージャー
        |
        ├── Scene : 各シーンスクリプト
        |    ├── TitleScene : タイトルシーン
        |    ├── GameScene : ゲーム中シーン
        |    └── ResultScene : リザルトシーン
        |
        ├── UI : ユーザーインターフェーススクリプト
        |    └── StageTimeLimitUI : 一例、タイムリミットUI
        |
        ├── Utility : 汎用、有用スクリプト
        |    └── GameTimer : 他スクリプト内で時間カウントを行う際に使用
        ...
        
        
```
## 今後の流れ、スケジュール
- [X] どういうゲームを作るか決定
- [X] 企画、コーディング規約の決定
- [ ] 役割分担
- [ ] 初期実装 (α版みたいな。大まかに動くやつ)
- [ ] 追加作業 (キャラなど、肉付け。市販のゲームっぽくする)

## コーディング規約、チーム開発に役立つリンク、教訓
### コーディング規約
[coding_rule (from So16さん)](https://github.com/aaaaa0114/fighting-game/blob/main/imgs/coding_rule.pdf)
### 役立つものリンク
[git](https://git-scm.com/downloads)<br>
[gitでのチーム開発](https://qiita.com/siida36/items/880d92559af9bd245c34)<br>
[commmit messageの書き方](https://qiita.com/itosho/items/9565c6ad2ffc24c09364)
### 教訓 (from カプコンインターン)
>1. シーンファイル保存は一人のシーン保存管理係だけが行う<br>
>↑ 最終的にシーンを統合するという点では、個人的には1人1シーンを持って、他のシーンに干渉しないのもいいかも
>1. それぞれの担当物を全てprefabで保存する
>1. ディレクトリ構成、スクリプトのテンプレを作ると良い
>1. **何かしら心境変換が起こる要素を持つゲームが面白いゲーム**
## Unityのバージョン(自分のバージョン書いときます)
| ライブラリ | バージョン |
| ------------ | ------------ |

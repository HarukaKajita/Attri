# CLAUDE.md

このファイルは、このリポジトリでコードを扱う際にClaude Code (claude.ai/code) にガイダンスを提供します。

## プロジェクト概要

**Attri**は、時系列多次元データの処理と圧縮機能を内蔵したUnityベースの属性データ管理・圧縮システムです。CSV/JSONのインポート・エクスポートワークフローをサポートし、属性データ最適化のための解析ツールを提供します。

## 開発コマンド

### Unity プロジェクト操作
- **プロジェクトを開く**: Unity Editor 2022.3.22f1 (LTS) で開く
- **ビルド**: Unityの標準ビルドシステムを使用 (Build Settings → Build)
- **テスト**: Unity Test Framework を使用 (`com.unity.test-framework` 1.1.33)

### アセンブリ構造
- **Runtime Assembly**: `Attri.asmdef` (namespace: `Attri.Runtime`)
- **Editor Assembly**: `Attri.Editor.asmdef` (namespace: `Attri.Editor`) 
- **CSV Parser**: `CSVParser.asmdef` (namespace: `yotokun`)

## アーキテクチャ概要

### コアシステム設計
システムは圧縮機能を持つ**汎用属性パターン**に従います：

- **IAttribute Interface**: すべての属性タイプの契約を定義
- **AttributeBase<T>**: 共通機能を提供する汎用基底クラス
- **特化属性**: `FloatAttribute`, `IntAttribute`, `StringAttribute`
- **フレームベース構成**: 時系列データ用の `FrameData<T>` コンテナ

### データフローパイプライン
1. **インポート**: カスタムUnityインポーター経由でCSV/JSONファイル (`.attricsv`, `.attrijson`)
2. **処理**: 内部フレームベース表現に変換
3. **圧縮**: 圧縮アルゴリズムを適用 (`CompressorBase` 実装)
4. **ストレージ**: ScriptableObjectベースのアセット管理
5. **解析**: データ可視化・最適化のためのエディターツール

### 圧縮フレームワーク
- **CompressorBase**: 圧縮アルゴリズムの抽象基底クラス
- **特化コンプレッサー**: `FloatCompressor`, `DirectionCompressor`, `FixedPrecisionFloatCompressor`
- **CompressedContainer**: 圧縮属性データのストレージ
- **CompressionType**: アルゴリズム選択用の列挙型

## 主要ディレクトリ

- **Assets/Attri/Runtime/**: コアランタイム機能
  - `Attribute/`: 属性システム実装
  - `AttributeData/`: データコンテナと圧縮
  - `CSVParser/`: 高性能CSV解析
- **Assets/Attri/Editor/**: Unity Editor統合
  - `Analysis/`: データ解析・可視化ツール
  - `Importer/`: 属性ファイル用カスタムインポーター
- **Assets/Attri/Sample/**: テストデータセット (`.attricsv`, `.attrijson` ファイル)

## 重要な実装ノート

### ファイル形式サポート
- **CSV形式**: 多次元数値データ、フレーム毎にカンマ区切り
- **JSON形式**: メタデータ付き構造化属性データ
- **Unity Assets**: 処理済みデータ用ScriptableObjectベースコンテナ

### 依存関係
- **Unity URP**: Universal Render Pipeline 14.0.10
- **Newtonsoft JSON**: JSON シリアライゼーション用 (`com.unity.nuget.newtonsoft-json`)
- **CSV-CSharp**: 統合高性能CSV解析ライブラリ

### データ型
システムは多次元数値データを処理します（サンプルの典型例）：
```
0.36787739,0.42110351,-0.82905853  // 3Dベクトルデータ
0.31771868,0.51146656,-0.79840893  // フレーム毎属性
```

## 開発パターン

### 新しい属性タイプの追加
1. `IAttribute` インターフェースを実装
2. 型付き機能のため `AttributeBase<T>` を拡張
3. 対応するScriptableObjectアセットクラスを作成
4. 必要に応じて圧縮サポートを追加

### カスタム圧縮アルゴリズム
1. `CompressorBase` 抽象クラスを拡張
2. 圧縮・展開ロジックを実装
3. `CompressionType` 列挙型に登録
4. 圧縮パイプラインに追加

### エディターツール
- カスタムインポーターがファイル処理を自動実行
- 解析ツールがデータ品質メトリクスを提供
- 属性アセット用のインスペクターカスタマイズ
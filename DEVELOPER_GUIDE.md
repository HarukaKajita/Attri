# Attri Plugin - デベロッパーガイド

## 概要 (Overview)

Attriプラグインfor Unityは、Unity環境内で時系列またはシーケンシャルな属性データの管理、インポート、分析、および利用を目的として設計されたシステムです。様々なデータ型（float, int, string）、次元を扱うための柔軟なフレームワークを提供し、シリアライズと圧縮を通じて効率的なストレージを可能にします。データインポートと分析のためのカスタムエディタツールが含まれています。このプラグインは、アニメーションシーケンス、シミュレーション結果、センサーの読み取り値など、GameObjectsや他のUnity要素に動的なデータを関連付ける必要があるシナリオで特に役立ちます。

## 特徴 (Features)

- **属性管理 (Attribute Management):**
    - 名前、型（float, int, string）、および次元を持つ属性を定義します。
    - 時系列またはシーケンシャルな情報に適したフレームに属性データを整理します。
    - Unityエディタ内での容易な管理のために、属性をScriptableObjectsとして表現します。
- **データインポート (Data Import):**
    - 設定可能な解析オプション（例：ヘッダーのスキップ）を使用して、CSVファイル（`.csv`, `.attricsv`）からデータをインポートします。
    - Attri固有のJSON（`.attrijson`, `.json`）またはバイナリ（`.attri`）ファイルからデータをインポートします。
    - `ScriptedImporter`とスタック可能な`ImportProcessor`コンポーネントを使用した拡張可能なインポートパイプライン。
- **シリアライズ (Serialization):**
    - ランタイムパフォーマンスとコンパクトなストレージのためにMessagePackを使用した効率的なバイナリシリアライズ。
    - 中間的な人間が読める形式としてのJSONのサポート。
- **データストレージと整理 (Data Storage & Organization):**
    - インポートされた属性データを`Container` ScriptableObjects（通常、属性ごとにフレームごとに1つ）に保存します。
    - `Container`を`Sequence` ScriptableObjectsにグループ化して、属性の完全なデータシーケンスを表現します。
- **データ圧縮 (Data Compression):**
    - float値の精度を調整することによりデータサイズを削減するための組み込みfloat圧縮（固定精度）。
    - 圧縮パラメータは設定および保存可能です。
- **データ分析 (Data Analysis):**
    - float属性データを分析するためのツール。
    - 統計的要約（最小、最大、範囲、標準偏差、中央値）を計算します。
    - float値のビットレベル分析（符号、指数、仮数部）を実行します。
- **エディタ統合 (Editor Integration):**
    - サポートされているファイルタイプのカスタムインポーター。Unityプロジェクトウィンドウで表示されます。
    - 属性データを視覚化および分析するためのカスタムエディタウィンドウ（詳細は「エディタ機能」セクションを参照）。
- **ランタイムアクセス (Runtime Access):**
    - ランタイムスクリプト内で属性データ（フレーム、要素、コンポーネント）にアクセスするためのAPIを提供します。

## 使い方 (How to Use)
### データインポート (Importing Data)
Attriは、特別に命名されたファイルを通じて、またはインポーターアセットを手動で作成および設定することにより、データのインポートをサポートします。

#### CSVファイル (.csv, .attricsv) (CSV Files (.csv, .attricsv))
1.  **CSVの準備 (Prepare your CSV):**
    *   CSVファイルがプレーンテキストであることを確認してください。
    *   データは行と列で整理されている必要があります。
    *   プラグインは、カンマ (`,`) またはタブ (`\t`) 区切りの値を処理できます。ファイル拡張子（使用されている場合は `.csv` 対 `.tsv`）に基づいて自動検出を試みますが、`.attricsv` ファイルは通常カンマ区切りです。
    *   使用例 `my_attribute_data.attricsv`:
        ```csv
        # Frame 0
        1.0,2.0,3.0
        1.1,2.1,3.1
        # Frame 1
        1.5,2.5,3.5
        1.6,2.6,3.6
        ```
        *（注：フレームと要素にマッピングされる正確なCSV構造は、具体的な`CsvImportProcessor`実装を検査して確認する必要があります。この例では、各行が要素であり、フレームのセクションは特定のプロセッサロジックによって処理されるか、またはフレームごとに複数のCSVをインポートすることによって処理されると想定しています。これは、各`Container`がフレームアセットである`Sequence`および`Container`構造とより整合性があるように思われます。）*
        個々のフレームCSVのより可能性の高い構造（例：`AttributeName_FrameIndex.csv`）：
        ```csv
        # AttributeName_0.csv ("AttributeName"のフレーム0を表す)
        val1_comp1,val1_comp2,val1_comp3
        val2_comp1,val2_comp2,val2_comp3
        ```
2.  **Unityへのインポート (Import into Unity):**
    *   `.csv`または`.attricsv`ファイルをUnityプロジェクトの`Assets`フォルダに配置します。
    *   Unityの`CsvImporter`が自動的にファイルを認識して処理します。
    *   プロジェクトウィンドウでインポートされたアセットを選択し、インスペクタ経由でインポート設定を構成できます。これには、複数の`CsvImportProcessor`が定義されている場合に特定のものを選択したり、「最初の行をスキップ」などのオプションを設定したりすることが含まれる場合があります。
    *   インポーターは通常、`Container`アセット（フレームごとに1つ）と、これらのコンテナをグループ化する`Sequence`アセットを生成します。正確な出力はプロセッサのロジックによって異なります。（例：`capybara/N_1.0.singleassetcsv`サンプルは、ファイルごとに1つのアセットを生成する命名規則を示唆しています）。

#### 属性ファイル (.json, .attrijson, .attri) (Attribute Files (.json, .attrijson, .attri))
これらのファイルは、シリアライズされた属性データを表し、多くの場合、別のAttriセットアップからエクスポートされるか、外部で生成されます。
1.  **ファイルの準備 (Prepare your file):**
    *   **`.json` / `.attrijson`:** 人間が読めるJSON形式。構造は、`IAttribute`とその具象クラスによって定義されたシリアライズ形式と一致する必要があり、通常は属性オブジェクトの配列です。使用例 `my_attributes.attrijson`:
        ```json
        [
          {
            "name": "MyFloatAttribute",
            "frames": [
              {
                "elements": [ { "components": [1.0, 2.0, 3.0] }, { "components": [4.0, 5.0, 6.0] } ]
              },
              {
                "elements": [ { "components": [1.5, 2.5, 3.5] } ]
              }
            ],
            "$type": "Attri.Runtime.FloatAttribute" // 型情報が重要です
          }
        ]
        ```
    *   **`.attri`:** バイナリMessagePack形式。これはよりコンパクトで効率的ですが、人間が読むことはできません。
2.  **Unityへのインポート (Import into Unity):**
    *   ファイルを`Assets`フォルダに配置します。
    *   `AttributeImporter`がそれを処理します。
    *   `.json`または`.attrijson`ファイルの場合、インポーターは最初にJSONを内部バイナリ形式に変換します。
    *   バイナリデータは、`IAttribute`オブジェクトにデシリアライズされます。
    *   `AttributeImporter`に関連付けられた`AttributeImportProcessor`がこれらの属性を処理し、`AttributeAsset` ScriptableObjects（`FloatAttributeAsset`など）または`Sequence`および`Container`アセットをプロジェクトに作成する可能性があります。

### スクリプトでのデータアクセス (Accessing Data in Scripts)
データがインポートされ、`Sequence`または`AttributeAsset`オブジェクトがプロジェクトで利用可能になると、C#スクリプトでそれらにアクセスできます。

```csharp
using UnityEngine;
using Attri.Runtime; // Attriランタイムネームスペースを必ず含めてください

public class AttributeDataReader : MonoBehaviour
{
    public Sequence myFloatSequence; // インスペクタでSequenceアセットを割り当てます
    public AttributeAsset myAttributeAsset; // または直接AttributeAssetを割り当てます

    void Start()
    {
        if (myFloatSequence != null)
        {
            Debug.Log($"Reading sequence: {myFloatSequence.attributeName}");
            Debug.Log($"Attribute type: {myFloatSequence.GetAttributeType()}");
            Debug.Log($"Frame count: {myFloatSequence.FrameCount}");

            // 例：データを3D float配列[frame][element][component]として取得
            float[][][] allFramesData = myFloatSequence.AsFloat();
            
            if (allFramesData.Length > 0 && allFramesData[0].Length > 0)
            {
                // 最初のフレームの最初の要素の最初のコンポーネントのデータにアクセス
                float firstComponent = allFramesData[0][0][0];
                Debug.Log($"First component of first element in first frame: {firstComponent}");
            }
        }

        if (myAttributeAsset != null)
        {
            IAttribute attribute = myAttributeAsset.GetAttribute();
            Debug.Log($"Reading attribute asset: {attribute.Name()}");
            Debug.Log($"Frame count: {attribute.FrameCount()}");

            // 例：最初のフレームのオブジェクトデータを取得
            // List<List<object>> frame0Data = attribute.GetObjectFrame(0);
            // 必要に応じてframe0Dataを処理...
        }
    }
}
```
*（注：複数フレーム、複数要素、複数コンポーネントのデータにアクセスするための`IAttribute`対`Sequence`の正確なAPIは再確認する必要があります。`Sequence.AsFloat()`はfloatシーケンスの直接的な方法を提供します。`IAttribute.GetObjectFrame()`はより汎用的です。）*

## コアコンセプトとアーキテクチャ (Core Concepts & Architecture)
### 主要なデータ構造 (Key Data Structures)

-   **`IAttribute` (インターフェース):**
    -   すべての属性タイプの基本的な契約。
    -   名前、型、次元、フレーム数、および生データを取得するためのメソッドを定義します。
    -   シリアライズ中のポリモーフィズムをサポートするために、MessagePackの`Union`属性と共に使用されます。
    -   実装: `AttributeBase<T>`, `FloatAttribute`, `IntAttribute`, `StringAttribute`。

-   **`AttributeBase<T>` (抽象クラス):**
    -   `IAttribute`の汎用的な基本実装。
    -   `name`（文字列）と`frames`（`FrameData<T>`オブジェクトのリスト）を格納します。
    -   属性タイプに共通の機能を提供します。

-   **`FloatAttribute`, `IntAttribute`, `StringAttribute` (具象クラス):**
    -   それぞれ`float`、`int`、`string`データ型に対する`AttributeBase<T>`の具体的な実装。
    -   型固有のシリアライズとバイト変換を処理します。

-   **`FrameData<T>` (クラス):**
    -   属性内の単一フレームのデータを表します。
    -   `Value<T>`オブジェクトの配列を含み、各`Value<T>`はそのフレーム内の1つの要素のコンポーネントを保持します。
    -   例：3Dベクター属性の場合、`FrameData<float>`は`Value<float>`オブジェクトを保持し、各`Value<float>`は3つのfloatコンポーネントを含みます。

-   **`Value<T>` (クラス):**
    -   フレーム内の単一の複数コンポーネント値を表します。（例：単一のVector3、単一のRGBAカラー）。
    -   実際のデータ値のために`T[] components`配列を保持します。

-   **`AttributeAsset` (抽象ScriptableObject):**
    -   `IAttribute`をUnity ScriptableObjectアセットとして表すための基本クラス。
    -   `FloatAttributeAsset`、`IntAttributeAsset`などの具象バージョンは、特定の属性タイプを格納します。Unityエディタで属性を直接管理できます。

-   **`ContainerBase` (抽象ScriptableObject):**
    -   属性データのコンテナを表し、通常は特定の属性の単一フレーム用です。
    -   特定の`AttributeDataType`（Float、Int、String）のデータを格納します。
    -   このデータに型付けされた配列（例：`ElementsAsFloat()`）としてアクセスするためのメソッドを提供します。

-   **`Container` (クラス、`ContainerBase`から派生したと想定):**
    -   属性の1フレームの実際の要素データ（例：`FloatElement`の配列）を格納する可能性が高い具象ScriptableObject。
    -   これらは、属性ごとにサブフォルダによく見られるアセットで、`AttributeName_FrameIndex.asset`のように命名されます。

-   **`Sequence` (ScriptableObject):**
    -   時間順に並べられた属性データのシーケンスを表します。
    -   `Container`アセットのリストを保持し、各`Container`はシーケンス内のフレームに対応します。
    -   シーケンスデータ全体にアクセスするための統一された方法を提供するために`IDataProvider`を実装します（例：`AsFloat()`は`float[][][]`を返します）。
    -   `GatherContainer()`エディタメソッドは、指定されたプロジェクトフォルダ内の関連する`Container`アセットを検索することにより、`containers`リストを自動的に設定します。

-   **`ElementBase` (抽象クラス):**
    -   `Container`内の単一データ要素を表し、複数のコンポーネントを持つことができます（例：3Dベクターは3つのfloatコンポーネントを持つ要素です）。
    -   コンポーネント数を取得し、型付けされた配列としてコンポーネントにアクセスするためのメソッドを定義します。

-   **`FloatElement`, `IntElement`, `StringElement` (具象クラス):**
    -   それぞれ`float[]`、`int[]`、`string[]`コンポーネントに対する`ElementBase`の具体的な実装。

### データフロー (Data Flow)

1.  **入力 (Input):** データは外部ファイルから取得されます：
    *   CSV (`.csv`, `.attricsv`)
    *   JSON (`.json`, `.attrijson`)
    *   バイナリAttri (`.attri`)

2.  **インポートと解析 (エディタ) (Import & Parsing (Editor)):**
    *   **`CsvImporter`:**
        *   `.csv`および`.attricsv`ファイルを処理します。
        *   `CSVParser`（yutokun製）を使用して、生テキストを`List<List<string>>`に解析します。
        *   `CsvImportProcessor`(s) がこの文字列データを変換します。これには通常、以下が含まれます：
            *   行/列を要素およびコンポーネントとして解釈します。
            *   文字列値をターゲットタイプ（float、int、string）に変換します。
            *   `Container` ScriptableObjectアセットを作成します。各アセットはCSVから抽出された1フレームのデータを表す場合があります。
            *   オプションで、同じ属性に対して複数のフレームがインポートされた場合にこれらの`Container`をグループ化するための`Sequence`アセットを作成します。
    *   **`AttributeImporter`:**
        *   `.json`、`.attrijson`、および`.attri`ファイルを処理します。
        *   JSONの場合、`AttributeSerializer.ConvertFromJson()`がテキストをMessagePackバイト配列に変換します。
        *   `AttributeSerializer.DeserializeAsArray()`がバイト配列を`List<IAttribute>`オブジェクトに変換します。
        *   `AttributeImportProcessor`(s) がこれらの`IAttribute`オブジェクトを受け取り、以下を行います：
            *   `AttributeAsset`（例：`FloatAttributeAsset`）を作成する場合があります。
            *   または、CSVパイプラインと同様に、データをプロジェクト内でより構造化されたフレームごとの方法で保存するために、それらを`Container`および`Sequence`アセットに変換する場合があります。

3.  **アセット作成 (エディタ) (Asset Creation (Editor)):**
    *   インポートプロセスにより、プロジェクト内にScriptableObjectsが作成されます：
        *   `Container`アセット：個々のフレームのデータを保存します。
        *   `Sequence`アセット：属性の`Container`をグループ化します。
        *   `AttributeAsset`アセット：`IAttribute`インスタンスを直接保存します。
        *   `CompressionParams`アセット：圧縮データの保存設定。

4.  **シリアライズ (ランタイム＆エディタ) (Serialization (Runtime & Editor)):**
    *   `AttributeSerializer`が中心的です。
    *   `IAttribute`および関連タイプ（`FrameData`、`Value`）の効率的なバイナリシリアライズ/デシリアライズにMessagePackを使用します。
    *   これは`AttributeImporter`によって使用され、必要に応じてランタイムで属性を保存/ロードするためにも使用される可能性があります。

5.  **圧縮 (エディタ/ランタイム) (Compression (Editor/Runtime)):**
    *   `FloatCompressor`（および潜在的に他のコンプレッサー）を使用してデータサイズを削減できます。
    *   元のfloatデータ（`float[][][]`）を受け取り、最適なパラメータを計算（または指定されたものを使用）し、圧縮されたfloatデータを生成します。
    *   圧縮パラメータ（`EncodeParam`、`DecodeParam`）は`CompressionParams`アセットとして保存できます。
    *   解凍は、圧縮データにアクセスするときにランタイムで行われ、保存されたパラメータを使用します。

6.  **分析 (エディタ/ランタイム) (Analysis (Editor/Runtime)):**
    *   `FloatAnalysis`などのクラスがデータ（例：`Sequence`または`IAttribute`からの`float[][][]`）を処理します。
    *   統計情報とビットレベル情報を計算します。
    *   これは、検査のためにカスタムエディタウィンドウで使用したり、データ駆動型ロジックのためにランタイムで使用したりすることができます。

7.  **ランタイムアクセス (Runtime Access):**
    *   スクリプトは、主に`Sequence`アセット（`AsFloat()`、`AsInt()`などのメソッドを使用）または`AttributeAsset`アセット（`GetAttribute()`を使用し、次に`IAttribute`メソッドを使用）を介してデータにアクセスします。
    *   データは通常、多次元配列（例：`float[frame][element][component]`）として提示されます。

## エディタ機能 (Editor Functionality)

Attriプラグインは、Unityエディタをいくつかのカスタムツールで拡張し、データのインポート、管理、分析を容易にします。

### インポーター (Importers)
さまざまなファイルタイプに対してカスタムインポーターが提供されており、Unityがそれらを認識して使用可能なアセットに処理できるようにします。これらのインポーターは、プロジェクトウィンドウで対応するファイルタイプが選択されたときに、インスペクタ経由で設定できます。

-   **`CsvImporter`:**
    -   **ファイル拡張子:** `.csv`, `.attricsv`
    -   **機能:** CSVファイルから表形式のデータをインポートします。CSVからの生の文字列データをAttriの内部データ構造（通常は`Container`および`Sequence`アセット）に変換するために、`CsvImportProcessor`のスタックと連携して柔軟に動作するように設計されています。
    -   **設定:**
        -   **プロセッサ:** インスペクタでさまざまな`CsvImportProcessor`を追加および設定できます。各プロセッサは、CSVデータの特定の側面または特定の変換を処理する場合があります。
        -   共通オプション（プロセッサによって異なる場合があります）：
            -   `Skip First Line`: CSVのヘッダー行を無視します。
            -   区切り文字設定（多くの場合自動検出されます）。
            -   ターゲット属性名、データ型など。

-   **`AttributeImporter`:**
    -   **ファイル拡張子:** `.json`, `.attrijson`, `.attri`
    -   **機能:** シリアライズされた`IAttribute`データを直接表すファイルをインポートします。
        -   `.json`および`.attrijson`ファイルの場合、最初に`AttributeSerializer`を使用してJSONテキストをMessagePackバイナリ形式に変換します。
        -   `.attri`ファイル（すでにバイナリMessagePack）の場合、それらを直接読み取ります。
        -   バイナリデータは、`IAttribute`オブジェクトのリストにデシリアライズされます。
        -   これらの`IAttribute`オブジェクトは、`AttributeImportProcessor`のスタックに渡されます。
    -   **設定:**
        -   **プロセッサ:** `CsvImporter`と同様に、インスペクタで`AttributeImportProcessor`を設定できます。これらのプロセッサは、生の`IAttribute`オブジェクトをプロジェクト内のアセットに変換する方法（例：`AttributeAsset`の作成、または`Container`および`Sequence`アセットの生成）を決定します。

### インポートプロセッサ (Import Processors)
インポートプロセッサは、データインポートパイプラインの特定のステップを定義するScriptableObjectsです。これらは`CsvImporter`および`AttributeImporter`によって使用されます。

-   **`CsvImportProcessor` (基本クラス):**
    -   CSVデータを扱うプロセッサに共通の機能を提供します。
    -   具象実装では、CSVの行/列を`FrameData`、`Value`オブジェクト、そして最終的には`Container`アセットにマッピングする方法を定義します。
    -   *（具体的なCsvImportProcessorsの例は、知られていればここにリストされます。例：「SingleFrameCsvProcessor」、「MultiFrameCsvProcessor」）*

-   **`AttributeImportProcessor` (基本クラス):**
    -   デシリアライズされた`IAttribute`オブジェクトを扱うプロセッサに共通の機能を提供します。
    -   具象実装では、これらの`IAttribute`オブジェクトをプロジェクトアセットに変換する方法を定義します（例：`AttributeAssetFromIAttributeProcessor`、`SequenceFromIAttributeProcessor`）。
    -   *（具体的なAttributeImportProcessorsの例は、知られていればここにリストされます）*

-   **スタック可能な性質 (Stackable Nature):** どちらのインポータータイプもプロセッサの*スタック*をサポートしています。これは、複数のプロセッサを連鎖させることができ、あるプロセッサの出力が次のプロセッサの入力になることを意味します。これにより、複雑な多段階のインポートワークフローが可能になります。

### 分析ウィンドウ (Analysis Windows)
*（このセクションは、`Assets/Attri/Editor/Analysis`に`AnalysisWindow.cs`やさまざまな`View.cs`ファイルが存在することに基づいています。正確な機能は、エディタを実行するか、これらのUIファイルを読んで確認する必要があります。）*

Attriには、属性データを視覚化および分析するためのカスタムエディタウィンドウが含まれています。これらのツールは、開発者がデータ品質を検査し、分布を理解し、問題をデバッグするのに役立ちます。

-   **アクセス:** Unityメインメニュー経由でアクセスできる可能性が高いです（例：「Window > Attri > Analysis」など）。
-   **機能 (推測):**
    -   **データ視覚化:**
        -   フレームにわたる属性値のプロット。
        -   多次元属性のコンポーネントの表示。
        -   圧縮データとオリジナルデータの視覚化。
    -   **統計的要約:**
        -   選択した属性またはコンポーネントの最小、最大、平均、標準偏差の表示。
        -   `FloatAnalysis`からの結果の表示（例：指数範囲、ビットパターン）。
    -   **圧縮分析:**
        -   さまざまな圧縮設定の効果を検査するためのツール。
        -   情報損失を評価するためのオリジナルデータと圧縮データの視覚的比較。
    -   **データ選択:**
        -   プロジェクトから`Sequence`アセットまたは`Container`アセットを選択して分析ウィンドウにロードする機能。

### カスタムインスペクタ (Custom Inspectors)
-   **`SequenceInspector`:** `Sequence`アセットのカスタムインスペクタを提供します。
    -   シーケンス内の`Container`のリストを表示する可能性が高いです。
    -   属性名、型、総フレーム数などのメタデータを表示する場合があります。
    -   命名規則に基づいて指定されたフォルダから`Container`アセットを自動的に検索して割り当てる「Gather Container」ボタンが含まれています。
-   `Container`、`AttributeAsset`、`CompressionParams`などの他のアセットも、データや設定オプションをユーザーフレンドリーな方法で表示するためのカスタムインスペクタを持っている可能性が高いです。

## 開発状況 (Development Status)

*免責事項：このセクションの情報は、コードベースの現在の状態に基づいており、最新の開発計画を反映していない可能性があります。コア開発者は、課題トラッカーやプロジェクトのロードマップからの具体的な詳細でこのセクションを更新する必要があります。*

### 作業中 (Work in Progress)
*   **高度な分析ツール:** （特にfloatに関して）基礎的な分析ロジックは存在しますが、分析ウィンドウのUIと機能セットはまだ進化している可能性が高いです。より洗練された視覚化オプションや比較分析ツールが開発中かもしれません。*（典型的なプラグイン開発に基づく想定）*
*   **拡張された圧縮オプション:** 現在、固定精度の`FloatCompressor`が明らかです。他の圧縮アルゴリズムや型（例：整数、文字列、または異なるfloat圧縮戦略）のサポートが計画されているか、初期段階にあるかもしれません。*（想定）*
*   **追加のインポートプロセッサ:** `StackableImporter`システムは柔軟です。さまざまなデータレイアウトや特定のユースケースに対応する、より専門化された`CsvImportProcessor`や`AttributeImportProcessor`が開発中である可能性があります。*（想定）*
*   **ランタイムAPIの強化:** ランタイムで属性データをアクセスおよび操作するためのAPIが、より複雑なクエリやリアルタイムの変更のために拡張されるかもしれません。*（想定）*
*   **ドキュメントとサンプル:** （このガイドのような）ドキュメントを改善および拡張し、より包括的な使用例を提供する継続的な取り組み。*（アクティブなプロジェクトで一般的）*

### 既知のバグ (Known Bugs)
*   *（コードベースのスキャンだけでは特定のバグは明らかではありません。このセクションは、バグレポートやテストからの情報を持つ開発者によって入力されるべきです。）*
*   **プレースホルダー:** 既知のバグのリストについては、プロジェクトの課題トラッカーを確認してください。
*   **潜在的な領域:** 不正な形式のファイルのインポートプロセスにおけるエラー処理には、まだ完全にはカバーされていないエッジケースが存在する可能性があります。*（インポートが多いツールの一般的な所見）*
*   **潜在的な領域:** プロジェクトがサポートするすべてのUnityバージョンとの互換性は定期的に検証する必要があります。*（一般的な所見）*

### TODO (TODOs)
*   **（コア開発者の方へ：バックログから計画されている機能、拡張機能、リファクタリングタスクをこのリストに記入してください。）**
*   **CSVからフレーム/要素へのマッピングの改良:** 特に単一CSV内の複数フレームまたは複数要素データについて、さまざまな`CsvImportProcessor`実装に対して期待される正確なCSV構造を明確にし、文書化します。
*   **包括的な単体テスト:** さまざまな属性タイプ、インポートプロセッサ、圧縮、分析モジュールを含むすべてのコア機能に対する単体テストカバレッジを拡大します。*（一般的なベストプラクティス）*
*   **パフォーマンスプロファイリングと最適化:** 特に大規模なデータセットを使用して、データインポート、シリアライズ、およびランタイムアクセスの徹底的なパフォーマンステストを実施します。
*   **サンプルシーン:** Attriプラグインのさまざまなユースケースを示すサンプルシーンをさらに作成します（例：キャラクターのアニメーション、シミュレーションデータの視覚化、エフェクトの駆動）。
*   **詳細なインスペクタツールチップ:** より良いユーザビリティのために、すべてのカスタムインスペクタフィールドに包括的なツールチップを追加します。
*   **ユーザーフレンドリーなエラーメッセージ:** エディタツールおよびランタイムAPIのエラーメッセージを改善し、ユーザーにとってより有益なものにします。
*   **`ValueByte`のドキュメント:** `CompressorBase`で使用される`ValueByte`クラスは完全には調査されていませんでした。その構造と使用法を文書化することができます。
*   **`MessagePackGenerated`コードの調査:** `Assets/Attri/Runtime/Attribute/MessagePackGenerated`内のコード（例：フォーマッタ、リゾルバ）の目的と生成プロセスを文書化し、シリアライズの内部を理解したり新しいシリアライズ可能な型を追加したりしたい開発者のために提供します。

## コントリビューションガイドライン (Contribution Guidelines)

（コア開発者の方へ：Attriプラグインへのコントリビューションに関するガイドライン、例えばコーディング標準、プルリクエストの手順、問題報告や機能提案の方法などを追加してください。）

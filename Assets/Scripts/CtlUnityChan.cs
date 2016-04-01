using UnityEngine;
using System;
using System.Collections;
using live2d;

[ExecuteInEditMode]
public class CtlUnityChan : MonoBehaviour {

	public TextAsset mocFile; // mocファイル
	public TextAsset physicsFile;
	public Texture2D texture; // テクスチャファイル
	public TextAsset[] mtnFiles; // mtnファイル
	public int motionNo = 0;

	//private Rigidbody rbody;

	private Live2DModelUnity live2DModel; //Live2Dモデルクラス
	private Live2DMotion 		motion; // Live2Dモーションクラス
	private MotionQueueManager 	motionManager; // モーション管理クラス
	private int preMotionNo = 0;
	private MotionQueueManager 	motionManager2; // モーション管理クラス

	void Start ()
	{

		//rbody = GetComponent<Rigidbody> ();
		//初期化（Live2Dを使用する前に必ず１度だけ呼び出す）
		Live2D.init();

		//モデルデータを読み込む
		live2DModel = Live2DModelUnity.loadModel ( mocFile.bytes );

		// 描画モードを指定
		live2DModel.setRenderMode( Live2D.L2D_RENDER_DRAW_MESH );

		//テクスチャの関連付け
		live2DModel.setTexture( 0, texture );

		preMotionNo = motionNo;

		// モーションのインスタンスの作成（mtnの読み込み）と設定
		motion = Live2DMotion.loadMotion( mtnFiles[ motionNo ].bytes );
		motion.setLoop( true );

		// モーション管理クラスのインスタンスの作成
		motionManager = new MotionQueueManager();
		motionManager2 = new MotionQueueManager();

		// モーションの再生
		motionManager.startMotion( motion, false );

	}

	void Update ()
	{
		//表示位置と大きさの指定
		Matrix4x4 m1 = Matrix4x4.Ortho( 0, 900, 900, 0, -0.5f, 0.5f );
		Matrix4x4 m2 = transform.localToWorldMatrix;
		Matrix4x4 m3 = m2 * m1;
		live2DModel.setMatrix( m3 );		

		//float time = UtSystem.getUserTimeMSec() / 1000f;
		//live2DModel.setParamFloat( "PARAM_ANGLE_X", 30 * Mathf.Sin( time ), 1);
		//live2DModel.setParamFloat ("PARAM_ANGLE_X", 1, 0.5f);



		if (preMotionNo != motionNo) {
			preMotionNo = motionNo;

			motion = Live2DMotion.loadMotion( mtnFiles[ motionNo ].bytes );
			motion.setLoop( true );
			motionManager.startMotion( motion, false );
		}

		// 再生中のモーションからモデルパラメータを更新
		motionManager.updateParam( live2DModel );

		//rbody.velocity = Vector2.zero;
//		if (Input.GetKey(KeyCode.RightArrow)) {
//			rbody.velocity = new Vector2 (2, rbody.velocity.y);
//		} else if (Input.GetKey(KeyCode.LeftArrow)) {
//			rbody.velocity = new Vector2 (-2, rbody.velocity.y);
//		} else 
//		if (Input.GetKey(KeyCode.UpArrow)) {
//			rbody.velocity = new Vector2 (rbody.velocity.x, 2);
//		}else if (Input.GetKey(KeyCode.DownArrow)) {
//			rbody.velocity = new Vector2 (rbody.velocity.x, -2);
//		}

		// Jump : スペースキー or ジョイスティックボタン 3
		if( Input.GetButtonDown( "Jump" ) ){
			motion = Live2DMotion.loadMotion( mtnFiles[ 4 ].bytes );
			motionManager2.startMotion( motion, false );
		}
		motionManager2.updateParam( live2DModel );

		// Rトリガー・Lトリガーで目の開閉（Input設定変更が必要）
		float axis_3 = Input.GetAxis( "Mouse ScrollWheel" ) * 10;
		live2DModel.addToParamFloat( "PARAM_EYE_L_OPEN", axis_3, 1 );
		live2DModel.addToParamFloat( "PARAM_EYE_R_OPEN", axis_3, 1 );

		// ゲームコントローラで顔を動かす（加算）
		float targetX = Input.GetAxis( "Horizontal" );
		float targetY = Input.GetAxis( "Vertical" ) ;
		live2DModel.addToParamFloat( "PARAM_ANGLE_X", 30 * targetX, 1 );
		live2DModel.addToParamFloat( "PARAM_ANGLE_Y", 30 * targetY, 1 );
		live2DModel.addToParamFloat( "PARAM_ANGLE_Z", 10 * targetX, 1 );
		live2DModel.addToParamFloat( "PARAM_BODY_ANGLE_X", 3 * targetX, 1 );
		live2DModel.addToParamFloat( "PARAM_BODY_ANGLE_Y", 3 * targetY, 1 );

		//頂点の更新
		live2DModel.update();

		//モデルの描画
		live2DModel.draw();
	}
}

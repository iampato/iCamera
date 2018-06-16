package md5389d341a3721a90f1582757fd3f1ee17;


public class TtsProgressListener
	extends android.speech.tts.UtteranceProgressListener
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onDone:(Ljava/lang/String;)V:GetOnDone_Ljava_lang_String_Handler\n" +
			"n_onError:(Ljava/lang/String;)V:GetOnError_Ljava_lang_String_Handler\n" +
			"n_onStart:(Ljava/lang/String;)V:GetOnStart_Ljava_lang_String_Handler\n" +
			"";
		mono.android.Runtime.register ("Plugin.TextToSpeech.TtsProgressListener, Plugin.TextToSpeech", TtsProgressListener.class, __md_methods);
	}


	public TtsProgressListener ()
	{
		super ();
		if (getClass () == TtsProgressListener.class)
			mono.android.TypeManager.Activate ("Plugin.TextToSpeech.TtsProgressListener, Plugin.TextToSpeech", "", this, new java.lang.Object[] {  });
	}


	public void onDone (java.lang.String p0)
	{
		n_onDone (p0);
	}

	private native void n_onDone (java.lang.String p0);


	public void onError (java.lang.String p0)
	{
		n_onError (p0);
	}

	private native void n_onError (java.lang.String p0);


	public void onStart (java.lang.String p0)
	{
		n_onStart (p0);
	}

	private native void n_onStart (java.lang.String p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}

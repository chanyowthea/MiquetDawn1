using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum _LogType
{
	Assert = LogType.Assert,
	Error = LogType.Error,
	Exception = LogType.Exception,
	Log = LogType.Log,
	Warning = LogType.Warning,
}

public class Log
{
	public int count = 1;
	public _LogType logType;
	public string condition;
	public string stacktrace;
	public int sampleId;
	//public string   objectName="" ;//object who send error
	//public string   rootName =""; //root of object send error

	public Log CreateCopy()
	{
		return (Log)this.MemberwiseClone();
	}

	public float GetMemoryUsage()
	{
		return (float)(sizeof(int) +
		sizeof(_LogType) +
		condition.Length * sizeof(char) +
		stacktrace.Length * sizeof(char) +
		sizeof(int));
	}
}


public class Test : MonoBehaviour {

	private void Start()
	{
#if USE_OLD_UNITY
            Application.RegisterLogCallback(new Application.LogCallback(CaptureLog));
            Application.RegisterLogCallbackThreaded(new Application.LogCallback(CaptureLogThread));
#else
		//Application.logMessageReceived += CaptureLog ;
		Application.logMessageReceivedThreaded += CaptureLogThread;
#endif
		Debug.Log("test01"); 
	}



	void CaptureLog(string condition, string stacktrace, LogType type)
	{
		AddLog(condition, stacktrace, type);
	}

	void AddLog(string condition, string stacktrace, LogType type)
	{
		//float memUsage = 0f;
		//string _condition = "";
		//if (cachedString.ContainsKey(condition))
		//{
		//	_condition = cachedString[condition];
		//}
		//else
		//{
		//	_condition = condition;
		//	cachedString.Add(_condition, _condition);
		//	memUsage += (string.IsNullOrEmpty(_condition) ? 0 : _condition.Length * sizeof(char));
		//	memUsage += System.IntPtr.Size;
		//}
		//string _stacktrace = "";
		//if (cachedString.ContainsKey(stacktrace))
		//{
		//	_stacktrace = cachedString[stacktrace];
		//}
		//else
		//{
		//	_stacktrace = stacktrace;
		//	cachedString.Add(_stacktrace, _stacktrace);
		//	memUsage += (string.IsNullOrEmpty(_stacktrace) ? 0 : _stacktrace.Length * sizeof(char));
		//	memUsage += System.IntPtr.Size;
		//}
		//bool newLogAdded = false;

		//addSample();
		Log log = new Log()
		{
			logType = (_LogType)type,
			condition = condition,
			stacktrace = stacktrace,
			//sampleId = samples.Count - 1
		};
		//memUsage += log.GetMemoryUsage();
		////memUsage += samples.Count * 13 ;

		//logsMemUsage += memUsage / 1024 / 1024;

		//if (TotalMemUsage > maxSize)
		//{
		//	clear();
		//	Debug.Log("Memory Usage Reach" + maxSize + " mb So It is Cleared");
		//	return;
		//}

		//bool isNew = false;
		////string key = _condition;// + "_!_" + _stacktrace ;
		//if (logsDic.ContainsKey(_condition, stacktrace))
		//{
		//	isNew = false;
		//	logsDic[_condition][stacktrace].count++;
		//}
		//else
		//{
		//	isNew = true;
		//	collapsedLogs.Add(log);
		//	logsDic[_condition][stacktrace] = log;

		//	if (type == LogType.Log)
		//		numOfCollapsedLogs++;
		//	else if (type == LogType.Warning)
		//		numOfCollapsedLogsWarning++;
		//	else
		//		numOfCollapsedLogsError++;
		//}

		//if (type == LogType.Log)
		//	numOfLogs++;
		//else if (type == LogType.Warning)
		//	numOfLogsWarning++;
		//else
		//	numOfLogsError++;


		//logs.Add(log);
		//if (!collapse || isNew)
		//{
		//	bool skip = false;
		//	if (log.logType == _LogType.Log && !showLog)
		//		skip = true;
		//	if (log.logType == _LogType.Warning && !showWarning)
		//		skip = true;
		//	if (log.logType == _LogType.Error && !showError)
		//		skip = true;
		//	if (log.logType == _LogType.Assert && !showError)
		//		skip = true;
		//	if (log.logType == _LogType.Exception && !showError)
		//		skip = true;

		//	if (!skip)
		//	{
		//		if (string.IsNullOrEmpty(filterText) || log.condition.ToLower().Contains(filterText.ToLower()))
		//		{
		//			currentLog.Add(log);
		//			newLogAdded = true;
		//		}
		//	}
		//}

		//if (newLogAdded)
		//{
		//	calculateStartIndex();
		//	int totalCount = currentLog.Count;
		//	int totalVisibleCount = (int)(Screen.height * 0.75f / size.y);
		//	if (startIndex >= (totalCount - totalVisibleCount))
		//		scrollPosition.y += size.y;
		//}

		try
		{
			gameObject.SendMessage("OnLog", log);
		}
		catch (System.Exception e)
		{
			Debug.LogException(e);
		}
	}

	List<Log> threadedLogs = new List<Log>();

	void CaptureLogThread(string condition, string stacktrace, LogType type)
	{
		Log log = new Log() { condition = condition, stacktrace = stacktrace, logType = (_LogType)type };
		
		lock (threadedLogs)
		{
			threadedLogs.Add(log);
		}
	}

	void Update()
	{
		//fpsText = fps.ToString("0.000");
		//gcTotalMemory = (((float)System.GC.GetTotalMemory(false)) / 1024 / 1024);
		////addSample();
		//if (string.IsNullOrEmpty(scenes[Application.loadedLevel]))
		//	scenes[Application.loadedLevel] = Application.loadedLevelName;

		//float elapsed = Time.realtimeSinceStartup - lastUpdate;
		//fps = 1f / elapsed;
		//lastUpdate = Time.realtimeSinceStartup;
		//calculateStartIndex();
		//if (!opened && show)
		//{
		//	doShow();
		//}

		if (threadedLogs.Count > 0)
		{
			lock (threadedLogs)
			{
				for (int i = 0; i < threadedLogs.Count; i++)
				{
					Log l = threadedLogs[i];
					AddLog(l.condition, l.stacktrace, (LogType)l.logType);
				}
				threadedLogs.Clear();
			}
		}

//		float elapsed2 = Time.realtimeSinceStartup - lastUpdate2;
//		if (elapsed2 > 1)
//		{
//			lastUpdate2 = Time.realtimeSinceStartup;
//			//be sure no body else take control of log 
//#if USE_OLD_UNITY
//            Application.RegisterLogCallback(new Application.LogCallback(CaptureLog));
//            Application.RegisterLogCallbackThreaded(new Application.LogCallback(CaptureLogThread));
//#endif
//		}
	}


	void DrawLogs()
	{
		//GUILayout.BeginArea(logsRect, backStyle);

		//GUI.skin = logScrollerSkin;
		////setStartPos();
		//Vector2 drag = getDrag();

		//if (drag.y != 0 && logsRect.Contains(new Vector2(downPos.x, Screen.height - downPos.y)))
		//{
		//	scrollPosition.y += (drag.y - oldDrag);
		//}
		//scrollPosition = GUILayout.BeginScrollView(scrollPosition);

		//oldDrag = drag.y;


		//int totalVisibleCount = (int)(Screen.height * 0.75f / size.y);
		//int totalCount = currentLog.Count;
		///*if( totalCount < 100 )
		//	inGameLogsScrollerSkin.verticalScrollbarThumb.fixedHeight = 0;
		//else 
		//	inGameLogsScrollerSkin.verticalScrollbarThumb.fixedHeight = 64;*/

		//totalVisibleCount = Mathf.Min(totalVisibleCount, totalCount - startIndex);
		//int index = 0;
		//int beforeHeight = (int)(startIndex * size.y);
		////selectedIndex = Mathf.Clamp( selectedIndex , -1 , totalCount -1);
		//if (beforeHeight > 0)
		//{
		//	//fill invisible gap befor scroller to make proper scroller pos
		//	GUILayout.BeginHorizontal(GUILayout.Height(beforeHeight));
		//	GUILayout.Label("---");
		//	GUILayout.EndHorizontal();
		//}

		//int endIndex = startIndex + totalVisibleCount;
		//endIndex = Mathf.Clamp(endIndex, 0, totalCount);
		//bool scrollerVisible = (totalVisibleCount < totalCount);
		//for (int i = startIndex; (startIndex + index) < endIndex; i++)
		//{

		//	if (i >= currentLog.Count)
		//		break;
		//	Log log = currentLog[i];

		//	if (log.logType == _LogType.Log && !showLog)
		//		continue;
		//	if (log.logType == _LogType.Warning && !showWarning)
		//		continue;
		//	if (log.logType == _LogType.Error && !showError)
		//		continue;
		//	if (log.logType == _LogType.Assert && !showError)
		//		continue;
		//	if (log.logType == _LogType.Exception && !showError)
		//		continue;

		//	if (index >= totalVisibleCount)
		//	{
		//		break;
		//	}

		//	GUIContent content = null;
		//	if (log.logType == _LogType.Log)
		//		content = logContent;
		//	else if (log.logType == _LogType.Warning)
		//		content = warningContent;
		//	else
		//		content = errorContent;
		//	//content.text = log.condition ;

		//	GUIStyle currentLogStyle = ((startIndex + index) % 2 == 0) ? evenLogStyle : oddLogStyle;
		//	if (log == selectedLog)
		//	{
		//		//selectedLog = log ;
		//		currentLogStyle = selectedLogStyle;
		//	}
		//	else
		//	{
		//	}

		//	tempContent.text = log.count.ToString();
		//	float w = 0f;
		//	if (collapse)
		//		w = barStyle.CalcSize(tempContent).x + 3;
		//	countRect.x = Screen.width - w;
		//	countRect.y = size.y * i;
		//	if (beforeHeight > 0)
		//		countRect.y += 8;//i will check later why
		//	countRect.width = w;
		//	countRect.height = size.y;

		//	if (scrollerVisible)
		//		countRect.x -= size.x * 2;

		//	Sample sample = samples[log.sampleId];
		//	fpsRect = countRect;
		//	if (showFps)
		//	{
		//		tempContent.text = sample.fpsText;
		//		w = currentLogStyle.CalcSize(tempContent).x + size.x;
		//		fpsRect.x -= w;
		//		fpsRect.width = size.x;
		//		fpsLabelRect = fpsRect;
		//		fpsLabelRect.x += size.x;
		//		fpsLabelRect.width = w - size.x;
		//	}

		//	memoryRect = fpsRect;
		//	if (showMemory)
		//	{
		//		tempContent.text = sample.memory.ToString("0.000");
		//		w = currentLogStyle.CalcSize(tempContent).x + size.x;
		//		memoryRect.x -= w;
		//		memoryRect.width = size.x;
		//		memoryLabelRect = memoryRect;
		//		memoryLabelRect.x += size.x;
		//		memoryLabelRect.width = w - size.x;
		//	}
		//	sceneRect = memoryRect;
		//	if (showScene)
		//	{
		//		tempContent.text = scenes[sample.loadedScene];
		//		w = currentLogStyle.CalcSize(tempContent).x + size.x;
		//		sceneRect.x -= w;
		//		sceneRect.width = size.x;
		//		sceneLabelRect = sceneRect;
		//		sceneLabelRect.x += size.x;
		//		sceneLabelRect.width = w - size.x;
		//	}
		//	timeRect = sceneRect;
		//	if (showTime)
		//	{
		//		tempContent.text = sample.time.ToString("0.000");
		//		w = currentLogStyle.CalcSize(tempContent).x + size.x;
		//		timeRect.x -= w;
		//		timeRect.width = size.x;
		//		timeLabelRect = timeRect;
		//		timeLabelRect.x += size.x;
		//		timeLabelRect.width = w - size.x;
		//	}


		//	GUILayout.BeginHorizontal(currentLogStyle);
		//	if (log == selectedLog)
		//	{
		//		GUILayout.Box(content, nonStyle, GUILayout.Width(size.x), GUILayout.Height(size.y));
		//		GUILayout.Label(log.condition, selectedLogFontStyle);
		//		//GUILayout.FlexibleSpace();
		//		if (showTime)
		//		{
		//			GUI.Box(timeRect, showTimeContent, currentLogStyle);
		//			GUI.Label(timeLabelRect, sample.time.ToString("0.000"), currentLogStyle);
		//		}
		//		if (showScene)
		//		{
		//			GUI.Box(sceneRect, showSceneContent, currentLogStyle);
		//			GUI.Label(sceneLabelRect, scenes[sample.loadedScene], currentLogStyle);
		//		}
		//		if (showMemory)
		//		{
		//			GUI.Box(memoryRect, showMemoryContent, currentLogStyle);
		//			GUI.Label(memoryLabelRect, sample.memory.ToString("0.000") + " mb", currentLogStyle);
		//		}
		//		if (showFps)
		//		{
		//			GUI.Box(fpsRect, showFpsContent, currentLogStyle);
		//			GUI.Label(fpsLabelRect, sample.fpsText, currentLogStyle);
		//		}


		//	}
		//	else
		//	{
		//		if (GUILayout.Button(content, nonStyle, GUILayout.Width(size.x), GUILayout.Height(size.y)))
		//		{
		//			//selectedIndex = startIndex + index ;
		//			selectedLog = log;
		//		}
		//		if (GUILayout.Button(log.condition, logButtonStyle))
		//		{
		//			//selectedIndex = startIndex + index ;
		//			selectedLog = log;
		//		}
		//		//GUILayout.FlexibleSpace();
		//		if (showTime)
		//		{
		//			GUI.Box(timeRect, showTimeContent, currentLogStyle);
		//			GUI.Label(timeLabelRect, sample.time.ToString("0.000"), currentLogStyle);
		//		}
		//		if (showScene)
		//		{
		//			GUI.Box(sceneRect, showSceneContent, currentLogStyle);
		//			GUI.Label(sceneLabelRect, scenes[sample.loadedScene], currentLogStyle);
		//		}
		//		if (showMemory)
		//		{
		//			GUI.Box(memoryRect, showMemoryContent, currentLogStyle);
		//			GUI.Label(memoryLabelRect, sample.memory.ToString("0.000") + " mb", currentLogStyle);
		//		}
		//		if (showFps)
		//		{
		//			GUI.Box(fpsRect, showFpsContent, currentLogStyle);
		//			GUI.Label(fpsLabelRect, sample.fpsText, currentLogStyle);
		//		}
		//	}
		//	if (collapse)
		//		GUI.Label(countRect, log.count.ToString(), barStyle);
		//	GUILayout.EndHorizontal();
		//	index++;
		//}

		//int afterHeight = (int)((totalCount - (startIndex + totalVisibleCount)) * size.y);
		//if (afterHeight > 0)
		//{
		//	//fill invisible gap after scroller to make proper scroller pos
		//	GUILayout.BeginHorizontal(GUILayout.Height(afterHeight));
		//	GUILayout.Label(" ");
		//	GUILayout.EndHorizontal();
		//}

		//GUILayout.EndScrollView();
		//GUILayout.EndArea();

		//buttomRect.x = 0f;
		//buttomRect.y = Screen.height - size.y;
		//buttomRect.width = Screen.width;
		//buttomRect.height = size.y;

		//if (showGraph)
		//	drawGraph();
		//else
		//	drawStack();
	}
}

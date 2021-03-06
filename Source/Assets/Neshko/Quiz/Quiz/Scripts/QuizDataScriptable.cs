﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionsData", menuName = "QuestionsData", order = 1)]
public class QuizDataScriptable : ScriptableObject
{
    public string MonumentNumber;
    public string categoryName;
    public string timeToComplete;
    public List<Question> questions;
}

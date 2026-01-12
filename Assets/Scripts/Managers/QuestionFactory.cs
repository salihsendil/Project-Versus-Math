using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[Serializable]
public struct QuestionData
{
    public string QuestionText;
    public int CorrectAnswer;
    public List<int> Answers;

    public QuestionData(string questionText, int correctAnswer, List<int> answers)
    {
        QuestionText = questionText;
        CorrectAnswer = correctAnswer;
        Answers = answers;
    }
}

public class QuestionFactory : IInitializable, IDisposable
{
    //References
    [Inject] private GameConfigSO gameConfigSO;

    //Operations
    private List<OperationTypes> allowedOperations = new();

    public void Initialize()
    {
        SetAllowedOperationList();
    }

    public void Dispose()
    {

    }

    public QuestionData GetNewQuestion()
    {
        OperationTypes operation = GetRandomOperation();
        QuestionData newQuestion = GenerateQuestion(operation);
        return newQuestion;
    }

    private QuestionData GenerateQuestion(OperationTypes operation)
    {
        int a = 0;
        int b = 0;
        int correct = 0;
        string question = "";

        int min = gameConfigSO.MinNumberLimit;
        int max = gameConfigSO.MaxNumberLimit;

        switch (operation)
        {
            case OperationTypes.Addition:
                a = UnityEngine.Random.Range(min, max);
                b = UnityEngine.Random.Range(min, max);
                correct = a + b;
                question = a + " + " + b + " = ?";
                break;

            case OperationTypes.Subtraction:
                a = UnityEngine.Random.Range(min, max);
                b = UnityEngine.Random.Range(min, a + 1);
                correct = a - b;
                question = a + " - " + b + " = ?";
                break;

            case OperationTypes.Multiplication:
                a = UnityEngine.Random.Range(min, max);
                b = UnityEngine.Random.Range(min, max);
                correct = a * b;
                question = a + " x " + b + " = ?";
                break;

            case OperationTypes.Division:
                GenerateDivision(min, max, out a, out b, out correct);
                question = a + " ÷ " + b + " = ?";
                break;
        }

        List<int> answers = GenerateFakeAnswers(correct, 3, 7);
        answers.Add(correct);
        HelperUtilities.Shuffle(answers);
        return new QuestionData(question, correct, answers);
    }

    private void GenerateDivision(int min, int max, out int a, out int b, out int correct)
    {
        const int maxTries = 10;

        for (int i = 0; i < maxTries; i++)
        {
            a = UnityEngine.Random.Range(min, max);

            if (a == 0) continue;

            for (int divisor = min; divisor <= max; divisor++)
            {
                if (divisor == 0) continue;

                if (a % divisor == 0)
                {
                    b = divisor;
                    correct = a / b;

                    if (correct >= min && correct <= max)
                        return;
                }
            }
        }

        b = 1;
        correct = UnityEngine.Random.Range(min, max);
        a = correct * b;
    }

    private List<int> GenerateFakeAnswers(int correctAnswer, int count, int offset)
    {
        List<int> fakeAnswers = new List<int>();
        int min = Mathf.Max(0, correctAnswer - offset);
        int max = correctAnswer + offset;

        while (fakeAnswers.Count < count)
        {
            int candidate = UnityEngine.Random.Range(min, max + 1);

            if (candidate == correctAnswer)
                continue;

            if (fakeAnswers.Contains(candidate))
                continue;

            fakeAnswers.Add(candidate);
        }
        return fakeAnswers;
    }

    private OperationTypes GetRandomOperation()
    {
        int random = UnityEngine.Random.Range(0, allowedOperations.Count);
        return allowedOperations[random];
    }

    private void SetAllowedOperationList()
    {
        if (gameConfigSO == null) { return; }

        allowedOperations.Clear();

        if (gameConfigSO.HasAllowedAddition) allowedOperations.Add(OperationTypes.Addition);
        if (gameConfigSO.HasAllowedSubtraction) allowedOperations.Add(OperationTypes.Subtraction);
        if (gameConfigSO.HasAllowedMultiplication) allowedOperations.Add(OperationTypes.Multiplication);
        if (gameConfigSO.HasAllowedDivision) allowedOperations.Add(OperationTypes.Division);
    }
}

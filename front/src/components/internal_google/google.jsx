import React, { useState, useEffect, useRef } from "react";
import "./google.css";
import { analyzeApi } from "../../services/chatbotService";
import { postReview } from "../../services/reviewService";

const Google = () => {

  const [Keymot, Setkeymot] = useState("");
  const [NbSentences, SetNbsentences] = useState("");
  const [AnalysisResult, SetAnalysisResult] = useState(null);
  const [Content, SetContent] = useState("");
  const [nbEtoile, SetNbEtoile] = useState("");
  const userId = sessionStorage.getItem("userId");
  const [errorMessage, setErrorMessage] = useState("");
  const idIA = "2";

  const canvasHistogramRef = useRef(null);
  const canvasPieChartRef = useRef(null);

  const submithandle = async () => {
    try {
      const result = await analyzeApi(Keymot, NbSentences);
      if (result && result.sentiments) {
        SetAnalysisResult(result);
      } else {
        throw new Error("Aucune phrase retournée");
      }
    } catch (error) {
      console.error("Erreur lors de l'analyse :", error.message);
    }
  };

  const reviewSubmit = async (e) => {
    e.preventDefault();
    setErrorMessage("");

    try {
      if (Content.length === 0) {
        setErrorMessage("le commentaire ne peut pas etre vide ! ");
      } else {
        const result = await postReview(idIA, userId, Content, nbEtoile);
        if (result) setErrorMessage("soumission faite , Merci !");
        alert(" commentaire Publie , merci");
        SetContent("");
        SetNbEtoile("");
      }
    } catch (error) {
      console.error("Erreur lors de la soumission de la critique:", error);
      setErrorMessage(
        "Une erreur est survenue lors de l'envoi de votre critique."
      );
    }
  };

  useEffect(() => {
    if (AnalysisResult) {
      drawHistogram(AnalysisResult);
      drawPieChart(AnalysisResult);
    }
  }, [AnalysisResult]);
  const drawHistogram = (analysisResult) => {
    const canvas = canvasHistogramRef.current;
    const ctx = canvas.getContext("2d");

    if (!analysisResult || !analysisResult.sentiments) {
      console.error(
        "Aucun résultat d'analyse disponible pour le dessin de l'histogramme"
      );
      return;
    }

    // Compter les occurrences de sentiments
    const sentimentCounts = analysisResult.sentiments.reduce(
      (acc, sentiment) => {
        acc[sentiment] = (acc[sentiment] || 0) + 1;
        return acc;
      },
      { positif: 0, négatif: 0 }
    );

    // Dessiner l'histogramme
    const barWidth = 100;
    const barHeightMultiplier = 10;
    let x = 50;
    const positiveBarHeight = sentimentCounts.positif * barHeightMultiplier;
    const negativeBarHeight = sentimentCounts.négatif * barHeightMultiplier;
    ctx.clearRect(0, 0, canvas.width, canvas.height);

    // Dessiner les barres
    ctx.fillStyle = "green"; // Barres positives
    ctx.fillRect(
      x,
      canvas.height - positiveBarHeight,
      barWidth,
      positiveBarHeight
    );
    ctx.fillStyle = "black";
    ctx.fillText(
      "Positif",
      x + barWidth / 2 - 20,
      canvas.height - positiveBarHeight - 5
    );

    ctx.fillStyle = "red"; // Barres négatives
    ctx.fillRect(
      x + barWidth + 50,
      canvas.height - negativeBarHeight,
      barWidth,
      negativeBarHeight
    );
    ctx.fillStyle = "black";
    ctx.fillText(
      "Négatif",
      x + barWidth + 50 + barWidth / 2 - 20,
      canvas.height - negativeBarHeight - 5
    );
  };

  const drawPieChart = (analysisResult) => {
    const canvas = canvasPieChartRef.current;
    const ctx = canvas.getContext("2d");

    if (!analysisResult || !analysisResult.sentiments) {
      console.error(
        "Aucun résultat d'analyse disponible pour le dessin du diagramme circulaire"
      );
      return;
    }

    // Compter les occurrences de sentiments
    const sentimentCounts = analysisResult.sentiments.reduce(
      (acc, sentiment) => {
        acc[sentiment] = (acc[sentiment] || 0) + 1;
        return acc;
      },
      { positif: 0, négatif: 0 }
    );

    // Dessiner le diagramme circulaire
    const total = sentimentCounts.positif + sentimentCounts.négatif;
    const positivePercentage = (sentimentCounts.positif / total) * 100;
    const negativePercentage = (sentimentCounts.négatif / total) * 100;

    ctx.clearRect(0, 0, canvas.width, canvas.height);

    // Dessiner le secteur positif
    const positiveStartAngle = 0;
    const positiveEndAngle = (positivePercentage / 100) * Math.PI * 2;
    ctx.beginPath();
    ctx.moveTo(canvas.width / 2, canvas.height / 2);
    ctx.arc(
      canvas.width / 2,
      canvas.height / 2,
      100,
      positiveStartAngle,
      positiveEndAngle
    );
    ctx.fillStyle = "green";
    ctx.fill();
    ctx.closePath();

    // Dessiner le secteur négatif
    const negativeStartAngle = positiveEndAngle;
    const negativeEndAngle =
      (negativePercentage / 100) * Math.PI * 2 + positiveEndAngle;
    ctx.beginPath();
    ctx.moveTo(canvas.width / 2, canvas.height / 2);
    ctx.arc(
      canvas.width / 2,
      canvas.height / 2,
      100,
      positiveEndAngle,
      negativeEndAngle
    );
    ctx.fillStyle = "red";
    ctx.fill();
    ctx.closePath();
  };

  return (
    <div className="container">
      <div className="card">
        <h3>Modèle prêt à être testé</h3>
        <input
          type="text"
          className="form-control"
          placeholder="Mot-clé"
          value={Keymot}
          onChange={(e) => Setkeymot(e.target.value)}
          required
        />
        <select
          className="form-control"
          value={NbSentences}
          onChange={(e) => SetNbsentences(e.target.value)}
          required
        >
          <option value="5">5</option>
          <option value="10">10</option>
          <option value="15">15</option>
          <option value="20">20</option>
        </select>
        <button onClick={submithandle} className="btn custom-btn-primary">
          Analyser
        </button>
      </div>
      <div className="card results">
        <h4>Résultats:</h4>
        <canvas ref={canvasHistogramRef} width="400" height="300"></canvas>
        <canvas ref={canvasPieChartRef} width="400" height="300"></canvas>
      </div>
      <div className="card">
        <h3>Ajouter un commentaire</h3>
        <textarea
          rows="8"
          maxLength="500"
          wrap="hard"
          value={Content}
          onChange={(e) => SetContent(e.target.value)}
          required
        />
        <select
          className="form-control"
          value={nbEtoile}
          onChange={(e) => SetNbEtoile(e.target.value)}
          required
        >
          <option type="number" selected value="1">
            1
          </option>
          <option type="number" value="2">
            2
          </option>
          <option type="number" value="3">
            3
          </option>
          <option type="number" value="4">
            4
          </option>
          <option type="number" value="5">
            5
          </option>
        </select>
        <button onClick={reviewSubmit} className="btn custom-btn-primary">
          Envoyer
        </button>
        {errorMessage && <p className="error-message">{errorMessage}</p>}
      </div>
    </div>
  );
};

export default Google;

import React, { useState } from "react";
import "./Cards.css";
import deep from "../../assets/Deep-Learning-4.jpeg";
import ml from "../../assets/ML1.jpg";
import NLP from "../../assets/NLP.jpg";
import robot from "../../assets/robot.jpeg";
import tf from "../../assets/TF.webp";
import llm from "../../assets/llm.jpg"

const Modal = ({ isOpen, close, title, detailedText }) => {
  if (!isOpen) return null;

  return (
    <div className="modal" onClick={close}>
      <div className="modal-content" onClick={(e) => e.stopPropagation()}>
        <span className="modal-close" onClick={close}>
          &times;
        </span>
        <h4>{title}</h4>
        <p>{detailedText}</p>
      </div>
    </div>
  );
};

const Card = ({ image, title, text, detailedText }) => {
  const [modalOpen, setModalOpen] = useState(false);

  const openModal = () => setModalOpen(true);
  const closeModal = () => setModalOpen(false);

  return (
    <div className="col-md-4">
      <div className="card">
        <img src={image} className="card-img-top" alt={title} />
        <div className="card-body">
          <h5 className="card-title">{title}</h5>
          <p className="card-text">{text}</p>
          <button onClick={openModal} className="btn btn-primary">
            Go to {title}
          </button>
          <Modal
            isOpen={modalOpen}
            close={closeModal}
            title={title}
            detailedText={detailedText}
          />
        </div>
      </div>
    </div>
  );
};

const Cards = () => {
  // Example detailed text added, you should replace these with the actual detailed information you want to show.
  return (
    <section className="container my-5">
      <div className="row">
        <Card
          image={ml}
          title="Deep Learning"
          text="Le deep learning est une branche avancée de l'intelligence artificielle qui utilise 
          des réseaux de neurones profonds..."
          detailedText="pour imiter le traitement de l'information par le cerveau humain.
                        Capable d'apprendre directement à partir des données, il excelle dans la reconnaissance de
                        motifs complexes et est essentiel dans des applications comme la reconnaissance d'images, 
                        la traduction automatique, et les véhicules autonomes. Cette technologie auto-adaptative
                        améliore significativement la précision et l'efficacité des tâches d'IA"
        />
        <Card
          image={deep}
          title="Machine Learning"
          text="Le machine learning est une technologie clé de l'intelligence artificielle
          permettant aux ordinateurs d'apprendre à partir de données..."
          detailedText=" Sans être explicitement programmés
            pour chaque tâche, ils améliorent leurs performances grâce à l'expérience, identifiant des motifs
             et prenant des décisions. Utilisé dans la détection de fraude, les recommandations en ligne,
              et bien plus, le machine learning rend les applications plus intuitives et efficaces"
        />
        <Card
          image={NLP}
          title="Natural Language Processing"
          text="Le NLP, ou traitement du langage naturel, est une discipline de l'IA 
          qui permet aux machines de comprendre et..."
          detailedText=" d'interagir avec le langage humain. Utilisé
           dans les chatbots, la traduction automatique et les assistants vocaux, le NLP transforme 
           l'interaction homme-machine, rendant les technologies plus accessibles et naturelles 
           à utiliser."
        />
        <Card
          image={robot}
          title="Robotics and AI"
          text="La robotique et l'IA combinent la mécanique, l'électronique et
          l'informatique pour créer des machines capables d'exécuter..."
          detailedText=" des tâches de manière
            autonome. L'intégration de l'IA dans la robotique permet de développer des robots
             plus intelligents et adaptables, capables d'apprendre de leur environnement et 
             d'effectuer des tâches complexes, transformant ainsi de nombreux secteurs,
              notamment la fabrication, la santé et le transport."
        />
        <Card
          image={tf}
          title="Tensorflow"
          text="TensorFlow est une bibliothèque open source développée par Google pour le calcul numérique et l'apprentissage automatique..."
          detailedText="Elle facilite la création de systèmes d'IA avec des outils pour développer, entraîner et déployer des modèles de machine learning et deep learning. Sa flexibilité et sa capacité à traiter de vastes ensembles de données en font un choix populaire parmi les chercheurs et les développeurs"
        />

<Card
          image={llm}
          title="LLM"
          text="Les LLM, ou modèles de langage à grande échelle, sont des systèmes d'IA avancés conçus pour comprendre..." 
          
          detailedText="générer et interagir en langage naturel à un niveau humain. Basés sur des architectures de deep learning comme les réseaux de neurones transformer, ces modèles sont entraînés sur d'immenses corpus de texte, leur permettant de réaliser des tâches variées telles que la traduction automatique"
        />
      </div>
    </section>
  );
};

export default Cards;

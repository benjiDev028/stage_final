import React, { useState } from 'react';
import { sendMessage as sendChatMessage } from '../../services/chatbotService'; // Adjust the import path according to the actual location
import './chatbot.css';

const Chatbot = ({ isVisible, toggleVisibility }) => {
  const [messages, setMessages] = useState([]);
  const [userInput, setUserInput] = useState('');

  const sendMessage = async (e) => {
    e.preventDefault();
    const trimmedInput = userInput.trim();
    if (!trimmedInput) return;

    // Add the user's message to the display
    setMessages([...messages, { text: trimmedInput, isUser: true }]);

    try {
      // Call sendMessage from your service passing userInput
      const responseText = await sendChatMessage(trimmedInput);
      setMessages(prevMessages => [...prevMessages, { text: responseText, isUser: false }]);
    } catch (error) {
      console.error("Error sending message:", error);
    }

    setUserInput(''); // Reset user input
  };

  if (!isVisible) {
    return <button onClick={toggleVisibility} className="toggle-chat-btn">Show Chatbot</button>;
  }

  return (
    <div className="chatbot-container">
      <button onClick={toggleVisibility} className="toggle-chat-btn">Hide Chatbot</button>
      <div className="messages-container">
        {messages.map((message, index) => (
          <div key={index} className={`message ${message.isUser ? 'user' : 'bot'}`}>
            {message.text}
          </div>
        ))}
      </div>
      <form onSubmit={sendMessage} className="input-container">
        <input
          type="text"
          value={userInput}
          onChange={(e) => setUserInput(e.target.value)}
          placeholder="Type a message..."
        />
        <button type="submit">Send</button>
      </form>
    </div>
  );
};

// Parent component
const App = () => {
  const [isChatVisible, setIsChatVisible] = useState(false);

  const toggleChatVisibility = () => {
    setIsChatVisible(!isChatVisible);
  };

  return (
    <>
      <Chatbot isVisible={isChatVisible} toggleVisibility={toggleChatVisibility} />
    </>
  );
};

export default App;

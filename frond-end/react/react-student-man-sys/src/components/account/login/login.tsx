import { useState } from "react";

interface LoginFormProps {
    onLogin: (username: string, password: string) => void;
  }

const Login: React.FC<LoginFormProps> = ({onLogin}) => {

    const [username, setUsername] = useState<string>('');
  const [password, setPassword] = useState<string>('');

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    //onLogin(username, password);
    try {
        const response = await fetch('http://localhost:41913/api/account/login', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({ username, password }),
        });
  
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
  
        const data = await response.json();
        console.log('Success:', data);
        onLogin(username, password);

      } catch (error) {
        console.error('Error:', error);
      }
      
  };

    return (
        <section>
            <div className="form">
                <h3>LOGIN</h3>
                <form onSubmit={handleSubmit} action="Post">

                    <div>
                        <label htmlFor="username">Username:</label>
                        <input
                            type="text"
                            id="username"
                            value={username}
                            onChange={(e) => setUsername(e.target.value)}
                        />
                    </div>

                    <div>
                        <label htmlFor="password">Password:</label>
                        <input
                            type="password"
                            id="password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                    </div>

                    <button className="btn btn-primary" type="submit">Login</button>
                </form>
            </div>
        </section>
    )
}

export default Login
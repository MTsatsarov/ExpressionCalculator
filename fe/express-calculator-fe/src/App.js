import { useState } from "react"
import axios from 'axios';
function App() {
	const endpoint = "https://localhost:7261/api/calculator"
	const [expression,setExpression] = useState("")
	const [result,setResult] = useState("")

	const onClick = () => {
		axios.post(`${endpoint}`,{
		expression
		}).then(resp => {
			setResult(resp.data)
		})
	}

	const onChange = (e) => {
		setExpression(e.target.value)
	}

	return (
		<div className="App" style={ { height: '1080px',display: 'flex',alignItems: "center",justifyContent: 'center' } } >
			<div style={ { width: '40%',height: '300px',border: '2px ridge black',display: 'flex',alignItems: 'center' } } >
				<div style={ { display: 'flex',justifyContent: 'space-around',margin: 'auto',width: "100%" } }>
					<input
						style={ {
							height: '30px',
							width: '350px'
						} }
						value={ expression }
						onChange={ (e) => onChange(e) }
					/>

					<button
						style={ {
							border: 'none',
							borderRadius: "10px",
							width: '120px',
							cursor: 'pointer',
							background: "#555553",
							color: 'white',
						} }
						onClick={ onClick }
					>
						Calculate
					</button>
					<input value={ result }></input>
				</div>
			</div>
		</div >
	);
}

export default App;

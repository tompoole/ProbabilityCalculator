import { useState, useEffect } from 'react';

export default function Home() {

  const [functions, setFunctions] = useState([]);
  const [result, setResult] = useState(null);
  const [error, setError] = useState(null);

  function fetchFunctions() {
    fetch("api/functions")
      .then(r => r.json())
      .then(d => setFunctions(d));
  }

  useEffect(() => {
    fetchFunctions();
  }, []);

  async function handleSubmit(e) {
    e.preventDefault();

    const formData = new FormData(e.target);
    const formObj = Object.fromEntries(formData);
    const request = `/api/functions/${formObj.function}?a=${formObj.a}&b=${formObj.b}`;

    const response = await fetch(request);
    const data = await response.json();

    if (response.ok) {
      setResult(data);
      setError(null);
    }
    else {
      setResult(null);
      setError(data);
    }
  }

  function renderResult(r) {
    return (
      <div className="card mb-3 col-4">
        <div className="card-header"> 
          Result
        </div>
        <div className="card-body">{r}</div>
      </div>);
  }

  function renderError(err) {
    return (
      <div className="card mb-3 col-4 text-bg-danger">
        <div className="card-header">
          Error!
        </div>
        <div className="card-body">{err}</div>
      </div>);
  }

  if (!functions.length) {
    return (<div>Loading...</div>);
  }

  const functionOptions = functions.map(f => <option key={f} value={f}>{f}</option>)
  const resultCard = result ? renderResult(result) : null;
  const errorCard = error ? renderError(error) : null;

  return (
    <>
      <form onSubmit={handleSubmit} className='col-4 mb-4'>
        <div className='mb-3'>
          <label className="form-label" htmlFor='function'>Function:</label>
          <select name='function' className='form-select'>
            {functionOptions}
          </select>
        </div>

        <div className='mb-3'>
          <label className="form-label" htmlFor='a'>Value A:</label>
          <input name='a' className='form-control' type='text' />
        </div>

        <div className='mb-3'>
          <label className="form-label" htmlFor='b'>Value B:</label>
          <input name='b' className='form-control' type='text' />
        </div>

        <button type='submit' className='btn btn-primary'>Calculate</button>
      </form>

      {resultCard}
      {errorCard}
    </>
    );
}
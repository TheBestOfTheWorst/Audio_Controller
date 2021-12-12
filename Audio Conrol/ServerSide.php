<!DOCTYPE HTML>
<html>
	<head>
    	<meta charset="UTF-8">
		<meta name="viewport" content="width=device-width, initial-scale=1.0">
    	<title>Volume Control</title>
	</head>
<body bgcolor="#7FFFD4">

<div style="position: absolute; top: 45%; left: 45%;">
	<form action="ServerSide.php" method="post">
		<input type="range" min="0" max="100" name="volume" step="1"/>
 		<input type="submit" name="button"/>
	</form>
</div>

<?php
	// если нажали кнопку, отправляем инфо
    if (isset($_POST['button']))
    {
		// эти строки нужны для правильного оформления url
		$curl_handle = curl_init();
		curl_setopt($curl_handle, CURLOPT_URL,'http://localhost:8888/connection/?value='.$_POST['volume']);
		curl_setopt($curl_handle, CURLOPT_CONNECTTIMEOUT, 2);
		curl_setopt($curl_handle, CURLOPT_RETURNTRANSFER, 1);
		curl_setopt($curl_handle, CURLOPT_USERAGENT, 'Audio Controller');
		$query = curl_exec($curl_handle);
		curl_close($curl_handle);
    }
?>	

</body>
</html>
<?php
header('Content-Type: text/plain');

// 確保請求是 POST
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    // 處理資料
    echo '1|OK'; // 回應第三方
} else {
    http_response_code(405);
    echo 'Method Not Allowed';
}
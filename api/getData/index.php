<?php
// 設定回應的 Content-Type 為 text/plain
header('Content-Type: text/plain');

// 檢查請求方法是否為 POST
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    // 在這裡處理你收到的通知數據
    // 如果一切順利，回應 1|OK
    echo '1|OK';
} else {
    // 如果請求不是 POST，或其他情況導致失敗
    // 回應 0|ErrorMessage
    http_response_code(400); // 設定適當的 HTTP 狀態碼
    echo '0|ErrorMessage';
}

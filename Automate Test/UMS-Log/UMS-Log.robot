# มอดูลตรวจสอบประวัติการใช้งานระบบ
*** Settings ***
Library     Selenium2Library

*** Variables ***
${ums_url}                          https://localhost:44346/
${ums_log_url}                      https://localhost:44346/Logs
${ums_login_input_email}            xpath=//*[@id="acc_Email"]
${ums_login_input_password}         xpath=//*[@id="acc_Password"]
${ums_btn_login}                    xpath=//*[@id="account"]/div[3]/div[2]/button
${log_input_message}                xpath=//*[@id="message"]
${log_input_date}                   xpath=//*[@id="message"]
${log_btn_search}                   xpath=//*[@id="searchLogs"]/div[3]/button[2]

*** Keywords ***
Open web browser
    Open Browser  ${ums_url}     chrome
    Maximize Browser Window

Login with "${username}" "${password}"
    Input Text          ${ums_login_input_email}        ${username}
    Input Password      ${ums_login_input_password}     ${password}
    sleep                       1s
    Click Element       ${ums_btn_login}

Go to logs monitor
    Go To   ${ums_log_url} 

Fill message "${message}"
    Input Text      ${log_input_message}    ${message}

Fill date "${date}"
    Input Text      ${log_input_message}    ${date}

Search
    click element               ${log_btn_search} 
    sleep                       1s

The alert message must say "${message}"
    Wait Until Page Contains    ${message}

*** Test Cases ***
UMS-Log-01
    [Documentation]     กรอกข้อความและวันที่ถูกต้อง
    [Tags]    PASS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to logs monitor
    AND Fill message "Welcome"
    AND Fill date "10/01/2020 12:00 AM - 10/01/2020 11:59 PM" 
    AND Search
    THEN The alert message must say "Successfully"
    [Teardown]    Close Browser

UMS-Log-02
    [Documentation]     กรอกข้อความและวันที่ไม่ถูกต้อง
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to logs monitor
    AND Fill message "**##@@"
    AND Fill date "**##@@" 
    THEN Search
    [Teardown]    Close Browser

UMS-Log-03
    [Documentation]     กรอกข้อความไม่ถูกต้อง
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to logs monitor
    AND Fill message "**##@@"
    AND Fill date "10/01/2020 12:00 AM - 10/01/2020 11:59 PM" 
    THEN Search
    [Teardown]    Close Browser

UMS-Log-04
    [Documentation]     กรอกวันที่ไม่ถูกต้อง
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to logs monitor
    AND Fill message "Welcome"
    AND Fill date "**##@@" 
    THEN Search
    [Teardown]    Close Browser

UMS-Log-05
    [Documentation]     ไม่กรอกข้อความและวันที่
    [Tags]    FAILS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to logs monitor
    THEN Search
    [Teardown]    Close Browser

UMS-Log-06
    [Documentation]     ไม่กรอกข้อความ แต่กรอกวันที่
    [Tags]    PASS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to logs monitor
    AND Fill date "10/01/2020 12:00 AM - 10/01/2020 11:59 PM" 
    AND Search
    THEN The alert message must say "Successfully"
    [Teardown]    Close Browser

UMS-Log-07
    [Documentation]     ไม่กรอกวันที่ แต่กรอกข้อความ
    [Tags]    PASS
    GIVEN Open web browser
    WHEN Login with "usermanagement2020@gmail.com" "123qweQ!"
    AND Go to logs monitor
    AND Fill message "Welcome"
    AND Search
    THEN The alert message must say "Successfully"
    [Teardown]    Close Browser
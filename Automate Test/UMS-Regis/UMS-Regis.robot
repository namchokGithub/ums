*** Settings ***
Library     Selenium2Library

*** Variables ***
${ums_url}                          https://localhost:44346/
${ums_regis_url}                    https://localhost:44346/Identity/Account/Register
${ums_login_input_email}            xpath=//*[@id="acc_Email"]
${ums_login_input_password}         xpath=//*[@id="acc_Password"]
${ums_btn_login}                    xpath=//*[@id="account"]/div[3]/div[2]/button
${regis_btn_back}                   xpath=//*[@id="btn_back"]
${regis_btn_register}               xpath=//*[@id="formRegister"]/button
${regis_input_firstname}            xpath=//*[@id="acc_Firstname"]
${regis_input_lastname}             xpath=//*[@id="acc_Lastname"]
${regis_input_email}                xpath=//*[@id="acc_Email"]
${regis_input_password}             xpath=//*[@id="acc_Password"]
${regis_input_confirmpass}          xpath=//*[@id="acc_ConfirmPassword"]

*** Keywords ***
Open web browser
    Open Browser  ${ums_regis_url}     chrome
    Maximize Browser Window

Login with "${username}" "${password}"
    Input Text          ${ums_login_input_email}        ${username}
    Input Password      ${ums_login_input_password}     ${password}
    sleep                       1s
    Click Element       ${ums_btn_login}

Go to Register
    sleep       1s
    Go to       ${ums_regis_url}

Fill firstname "${firstname}"
    Input Text      ${regis_input_firstname}    ${firstname}

Fill lastname "${lastname}"
    Input Text      ${regis_input_lastname}    ${lastname}

Fill email "${email}"
    Input Text      ${regis_input_email}    ${email}

Fill password "${password}"
    Input Password      ${regis_input_password}    ${password}

Fill confirmpass "${confirmpass}"
    Input Password      ${regis_input_confirmpass}    ${confirmpass}

Go back
    sleep                       1s
    Click Element       ${regis_btn_back}

Register
    sleep                       3s
    Click Element       ${regis_btn_register}
    sleep                       3s

The alert message must say "${message}"
    sleep                       1s
    Wait Until Page Contains    ${message}

Is Home Page?
    sleep                       1s
    Location Should Be     ${ums_url} 

*** Test Cases ***
UMS-Regis-01
    [Documentation]     กรอกชื่อจริง นามสกุล อีเมล รหัสผ่าน และยืนยันรหัสผ่านถูกต้อง
    [Tags]    PASS
    GIVEN Open web browser
    AND Fill firstname "Namchok"
    AND Fill lastname "Singhachai"
    AND Fill email "Test998@gmail.com"
    AND Fill password "123qweQ!"
    AND Fill confirmpass "123qweQ!"
    # AND Register
    # THEN Is Home Page?
    [Teardown]    Close Browser
UMS-Regis-02
    [Documentation]     กรอกชื่อจริง นามสกุล อีเมล รหัสผ่าน และยืนยันรหัสผ่านไม่ถูกต้อง
    [Tags]    FAILS
    GIVEN Open web browser
    AND Fill firstname "!Namchok"
    AND Fill lastname "!Singhachai"
    AND Fill email "Test999@gmail"
    AND Fill password "123qweQ"
    AND Fill confirmpass "123qweQ"
    AND Register
    THEN The alert message must say "The first name can not be blank and must only character."
    [Teardown]    Close Browser
UMS-Regis-03
    [Documentation]     กรอกชื่อจริงไม่ถูกต้อง
    [Tags]    FAILS
    GIVEN Open web browser
    AND Fill firstname "!Namchok"
    AND Fill lastname "Singhachai"
    AND Fill email "Test999@gmail.com"
    AND Fill password "123qweQ!"
    AND Fill confirmpass "123qweQ!"
    AND Register
    THEN The alert message must say "The first name can not be blank and must only character."
    [Teardown]    Close Browser
UMS-Regis-04
    [Documentation]     กรอกนามสกุลไม่ถูกต้อง
    [Tags]    FAILS
    GIVEN Open web browser
    AND Fill firstname "Namchok"
    AND Fill lastname "!Singhachai"
    AND Fill email "Test999@gmail.com"
    AND Fill password "123qweQ!"
    AND Fill confirmpass "123qweQ!"
    AND Register
    THEN The alert message must say "The last name can not be blank and must only character."
    [Teardown]    Close Browser
UMS-Regis-05
    [Documentation]     กรอกอีเมลไม่ถูกต้อง
    [Tags]    FAILS
    GIVEN Open web browser
    AND Fill firstname "Namchok"
    AND Fill lastname "Singhachai"
    AND Fill email "Test999@gmail"
    AND Fill password "123qweQ!"
    AND Fill confirmpass "123qweQ!"
    AND Register
    THEN The alert message must say "Invalid Emails."
    [Teardown]    Close Browser
UMS-Regis-06
    [Documentation]     กรอกรหัสผ่านไม่ถูกต้อง
    [Tags]    FAILS
    GIVEN Open web browser
    AND Fill firstname "Namchok"
    AND Fill lastname "Singhachai"
    AND Fill email "Test999@gmail.com"
    AND Fill password "123qweQ"
    AND Fill confirmpass "123qweQ!"
    AND Register
    THEN The alert message must say "The password must contain at least 1 uppercase, 1 lowercase, 1 digit and 1 special character."
    [Teardown]    Close Browser
UMS-Regis-07
    [Documentation]     กรอกยืนยันรหัสผ่านไม่ถูกต้อง
    [Tags]    FAILS
    GIVEN Open web browser
    AND Fill firstname "Namchok"
    AND Fill lastname "Singhachai"
    AND Fill email "Test999@gmail.com"
    AND Fill password "123qweQ!"
    AND Fill confirmpass "123qweQ"
    AND Register
    THEN The alert message must say "The current password must contain at least 1 uppercase, 1 lowercase, 1 digit and 1 special character."
    [Teardown]    Close Browser
UMS-Regis-08
    [Documentation]     ไม่กรอกชื่อจริง นามสกุล อีเมล รหัสผ่าน และยืนยันรหัสผ่าน
    [Tags]    FAILS
    GIVEN Open web browser
    AND Fill firstname " "
    AND Fill lastname " "
    AND Fill email " "
    AND Fill password " "
    AND Fill confirmpass " "
    AND Register
    THEN The alert message must say "The first name can not be blank and must only character."
    [Teardown]    Close Browser
UMS-Regis-09
    [Documentation]     กรอกนามสกุล อีเมล รหัสผ่าน และยืนยันรหัสผ่าน แต่ไม่กรอกชื่อจริง
    [Tags]    FAILS
    GIVEN Open web browser
    AND Fill firstname " "
    AND Fill lastname "Singhachai"
    AND Fill email "Test999@gmail.com"
    AND Fill password "123qweQ!"
    AND Fill confirmpass "123qweQ!"
    AND Register
    THEN The alert message must say "The first name can not be blank and must only character."
    [Teardown]    Close Browser
UMS-Regis-10
    [Documentation]     กรอกชื่อจริง อีเมล รหัสผ่าน และยืนยันรหัสผ่าน แต่ไม่กรอกนามสกุล
    [Tags]    FAILS
    GIVEN Open web browser
    AND Fill firstname "Namchok"
    AND Fill lastname " "
    AND Fill email "Test999@gmail.com"
    AND Fill password "123qweQ!"
    AND Fill confirmpass "123qweQ!"
    AND Register
    THEN The alert message must say "The last name can not be blank and must only character."
    [Teardown]    Close Browser
UMS-Regis-11
    [Documentation]     กรอกชื่อจริง นามสกุล รหัสผ่าน และยืนยันรหัสผ่าน แต่ไม่กรอกอีเมล
    [Tags]    FAILS
    GIVEN Open web browser
    AND Fill firstname "Namchok"
    AND Fill lastname "Singhachai"
    AND Fill email " "
    AND Fill password "123qweQ!"
    AND Fill confirmpass "123qweQ!"
    AND Register
    THEN The alert message must say "The Email field is required."
    [Teardown]    Close Browser
UMS-Regis-12
    [Documentation]     กรอกชื่อจริง นามสกุล อีเมล และยืนยันรหัสผ่าน แต่ไม่กรอกรหัสผ่าน
    [Tags]    FAILS
    GIVEN Open web browser
    AND Fill firstname "Namchok"
    AND Fill lastname "Singhachai"
    AND Fill email "Test999@gmail.com"
    AND Fill password " "
    AND Fill confirmpass "123qweQ!"
    AND Register
    THEN The alert message must say "The password must contain at least 1 uppercase, 1 lowercase, 1 digit and 1 special character."
    [Teardown]    Close Browser
UMS-Regis-13
    [Documentation]     กรอกชื่อจริง นามสกุล อีเมล และรหัสผ่าน แต่ไม่กรอกยืนยันรหัสผ่าน 
    [Tags]    FAILS
    GIVEN Open web browser
    AND Fill firstname "Namchok"
    AND Fill lastname "Singhachai"
    AND Fill email "Test999@gmail.com"
    AND Fill password "123qweQ!"
    AND Fill confirmpass " "
    AND Register
    THEN The alert message must say "The current password must contain at least 1 uppercase, 1 lowercase, 1 digit and 1 special character."
    [Teardown]    Close Browser
UMS-Regis-14
    [Documentation]     กรอกอีเมลที่ถูกใช้งานไปแล้ว
    [Tags]    FAILS
    GIVEN Open web browser
    AND Fill firstname "Namchok"
    AND Fill lastname "Singhachai"
    AND Fill email "pech4751@gmail.com"
    AND Fill password "123qweQ!"
    AND Fill confirmpass "123qweQ!"
    AND Register
    THEN The alert message must say "This user has been taken."
    [Teardown]    Close Browser